// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPFBrowser;

namespace IPFBrowser.FileFormats.IES
{
	public class IesFile : IDisposable
	{
		private Stream _stream;
		private BinaryReader _reader;
		private byte _xorKey;

        private const int HeaderNameLengths = 0x40;
        private const int ColumnSize = 136;
        private const int SizesPos = (2 * HeaderNameLengths + 2 * sizeof(short));

        public List<IesColumn> Columns { get; private set; }
		public IesHeader Header { get; private set; }
		public List<IesRow> Rows { get; private set; }

		public IesFile(Stream stream)
		{
			_stream = stream;
			_reader = new BinaryReader(_stream);
			_xorKey = 1;

			this.ReadHeader();
			this.ReadColumns();
			this.ReadRows();
		}

		public IesFile(byte[] content)
			: this(new MemoryStream(content))
		{
		}

		private string DecryptString(byte[] data, Encoding encoding = null)
		{
			if (encoding == null)
				encoding = Encoding.UTF8;

			var bytes = new byte[data.Length];
			for (int i = 0; i < data.Length; i++)
				bytes[i] = (byte)(data[i] ^ _xorKey);

			return encoding.GetString(bytes).TrimEnd(new char[] { '\x0001' });
		}

		public void Dispose()
		{
			if (_reader != null)
				_reader.Close();
		}

		private void ReadColumns()
		{
			_stream.Seek((-((long)this.Header.ResourceOffset) - this.Header.DataOffset), SeekOrigin.End);

			this.Columns = new List<IesColumn>();
			for (int i = 0; i < this.Header.ColumnCount; i++)
			{
				var item = new IesColumn();
				item.Name = this.DecryptString(_reader.ReadBytes(0x40), null);
				item.Name2 = this.DecryptString(_reader.ReadBytes(0x40), null);
				item.Type = (ColumnType)_reader.ReadUInt16();
                item.Access = (PropertyAccess)_reader.ReadUInt16();
                item.Sync = _reader.ReadUInt16();
                item.Position = _reader.ReadUInt16();

				// Duplicates
				var old = item.Name;
				for (int j = 1; this.Columns.Exists(a => a.Name == item.Name); ++j)
					item.Name = old + "_" + j;

				this.Columns.Add(item);
			}
			this.Columns.Sort();
		}

		private void ReadHeader()
		{
            this.Header = new IesHeader();
			this.Header.Name = Encoding.UTF8.GetString(_reader.ReadBytes(0x80));
            this.Header.Version = _reader.ReadUInt16();
            _reader.ReadUInt16(); // padding
            this.Header.DataOffset = _reader.ReadUInt32();
			this.Header.ResourceOffset = _reader.ReadUInt32();
			this.Header.FileSize = _reader.ReadUInt32();
            this.Header.UseClassId = (_reader.ReadByte() != 0);
            _reader.ReadByte(); // padding
            this.Header.RowCount = _reader.ReadUInt16();
			this.Header.ColumnCount = _reader.ReadUInt16();
			this.Header.NumberColumnCount = _reader.ReadUInt16();
			this.Header.StringColumnCount = _reader.ReadUInt16();
			_reader.ReadUInt16();
		}

		private void ReadRows()
		{
			_reader.BaseStream.Seek(-((long)this.Header.ResourceOffset), SeekOrigin.End);

			this.Rows = new List<IesRow>();
			for (int i = 0; i < this.Header.RowCount; ++i)
			{
                var item = new IesRow();

                item.ClassId = _reader.ReadInt32();
                item.ClassName = _reader.ReadXoredLpString();
                //var count = _reader.ReadUInt16();
				//_reader.ReadBytes(count);

				
				for (int j = 0; j < this.Columns.Count; ++j)
				{
					var column = this.Columns[j];

					if (column.IsNumber)
					{
						var nan = _reader.ReadSingle();
						item.Add(column.Name, nan);
					}
					else
					{
						var length = _reader.ReadUInt16();
						var str = "";
						if (length > 0)
							str = this.DecryptString(_reader.ReadBytes(length), null);

						item.Add(column.Name, str);
					}
				}

				this.Rows.Add(item);
				_reader.BaseStream.Seek((long)this.Header.StringColumnCount, SeekOrigin.Current);
			}
		}
        /// <summary>
        /// Saves this instance's data to IES file.
        /// </summary>
        /// <param name="filePath"></param>
        public byte[] ToBytes()
        {
            var columns = this.Columns.ToList();
            var sortedColumns = columns.OrderBy(a => a.IsNumber ? 0 : 1).ThenBy(a => a.Position);
            var rows = this.Rows;

            var rowCount = rows.Count;
            var colCount = columns.Count;
            var numberColCount = columns.Count(a => a.IsNumber);
            var stringColCount = colCount - numberColCount;

            MemoryStream ms = new MemoryStream();

            using (var bw = new BinaryWriter(ms))
            {
                bw.WriteFixedString(this.Header.Name, HeaderNameLengths * 2);
                bw.Write((ushort)this.Header.Version);
                bw.Write((ushort)0); // padding
                bw.Write((uint)this.Header.DataOffset);
                bw.Write((uint)this.Header.ResourceOffset);
                bw.Write((uint)this.Header.FileSize);
                bw.Write(this.Header.UseClassId ? (byte)1 : (byte)0);
                bw.Write((byte)0); // padding
                bw.Write((ushort)rowCount);
                bw.Write((ushort)colCount);
                bw.Write((ushort)numberColCount);
                bw.Write((ushort)stringColCount);
                bw.Write((ushort)0); // padding

                foreach (var column in columns)
                {
                    bw.WriteXoredFixedString(column.Name, HeaderNameLengths);
                    bw.WriteXoredFixedString(column.Name2, HeaderNameLengths);
                    bw.Write((ushort)column.Type);
                    bw.Write((ushort)column.Access);
                    bw.Write((ushort)column.Sync);
                    bw.Write((ushort)column.Position);
                }

                var rowsStart = bw.BaseStream.Position;
                foreach (var row in rows)
                {
					// Find the ClassId and ClassName
					// If these are part of the table, we write these instead of whatever
					// values we might have here, in case the user changed them
					if (row.TryGetValue("ClassID", out var classId))
					{
                        bw.Write((int)(float)classId);
                    }
					else
					{
                        bw.Write(row.ClassId);
                    }

					if (row.TryGetValue("ClassName", out var className))
					{
                        bw.WriteXoredLpString((String)className);
                    }
					else
					{
                        bw.WriteXoredLpString(row.ClassName ?? "");
                    }

                    foreach (var column in sortedColumns)
                    {
                        if (!row.TryGetValue(column.Name, out var value))
                        {
                            if (column.IsNumber)
                                bw.Write(0f);
                            else
                                bw.Write((ushort)0);
                        }
                        else
                        {
                            if (column.IsNumber)
                                bw.Write((float)value);
                            else
                                bw.WriteXoredLpString((string)value);
                        }
                    }

                    foreach (var column in sortedColumns.Where(a => !a.IsNumber))
                    {
                        if (row.UseScr.TryGetValue(column.Name, out var value))
                            bw.Write(value ? (byte)1 : (byte)0);
                        else
                            bw.Write((byte)0);
                    }
                }

                this.Header.DataOffset = (uint)(columns.Count * ColumnSize);
                this.Header.ResourceOffset = (uint)(bw.BaseStream.Position - rowsStart);
                this.Header.FileSize = (uint)bw.BaseStream.Position;

                bw.BaseStream.Seek(SizesPos, SeekOrigin.Begin);
                bw.Write((uint)this.Header.DataOffset);
                bw.Write((uint)this.Header.ResourceOffset);
                bw.Write((uint)this.Header.FileSize);
                bw.BaseStream.Seek(0, SeekOrigin.End);
            }

			return ms.ToArray();
        }
    }

	public class IesHeader
	{
		public int Version { get; set; }
        public uint DataOffset { get; set; }
		public uint ResourceOffset { get; set; }
		public uint FileSize { get; set; }
		public bool UseClassId { get; set; }
		public string Name { get; set; }
		public ushort ColumnCount { get; set; }
		public ushort RowCount { get; set; }
		public ushort NumberColumnCount { get; set; }
		public ushort StringColumnCount { get; set; }
	}

	public class IesColumn : IComparable<IesColumn>
	{
		public string Name { get; set; }
		public string Name2 { get; set; }
		public ColumnType Type { get; set; }
		public ushort Position { get; set; }
        public PropertyAccess Access { get; set; } = PropertyAccess.SP;
        public int Sync { get; set; }
        public bool IsNumber { get { return (this.Type == ColumnType.Float); } }

		public int CompareTo(IesColumn other)
		{
			if (((this.Type == other.Type) || ((this.Type == ColumnType.String) && (other.Type == ColumnType.String2))) || ((this.Type == ColumnType.String2) && (other.Type == ColumnType.String)))
				return this.Position.CompareTo(other.Position);

			if (this.Type < other.Type)
				return -1;

			return 1;
		}
	}

	public enum ColumnType
	{
		Float,
		String,
		String2
	}

    public enum PropertyAccess : byte
    {
        EP,
        CP,
        VP,
        SP,
        CT,
    }

    public class IesRow : Dictionary<string, object>
	{
        public int ClassId { get; set; }
        public string ClassName { get; set; }
        public Dictionary<string, bool> UseScr { get; } = new Dictionary<string, bool>();
        public float GetFloat(string name)
		{
			if (!ContainsKey(name))
				throw new ArgumentException("Unknown field: " + name);

			if (this[name] is float) return (float)this[name];
			if (this[name] is uint) return (float)(uint)this[name];

			throw new ArgumentException(name + " is not numeric");
		}

		public uint GetUInt(string name)
		{
			return (uint)GetInt(name);
		}

		public int GetInt(string name)
		{
			if (!ContainsKey(name))
				throw new ArgumentException("Unknown field: " + name);

			if (this[name] is float) return (int)(float)this[name];
			if (this[name] is uint) return (int)(uint)this[name];

			throw new ArgumentException(name + " is not numeric");
		}

		public string GetString(string name)
		{
			if (!ContainsKey(name))
				throw new ArgumentException("Unknown field: " + name);

			if (this[name] is string) return (string)this[name];

			throw new ArgumentException(name + " is not a string");
		}
	}
}
