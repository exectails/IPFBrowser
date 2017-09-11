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

using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace IPFBrowser.FileFormats.IPF
{
	public class Ipf
	{
		private object _extractLock = new object();
		private readonly Stream _stream;
		private readonly BinaryReader _br;

		public string FilePath { get; private set; }
		public List<IpfFile> Files { get; private set; }
		public IpfFooter Footer { get; private set; }

		public Ipf(string filePath)
		{
			_stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
			_br = new BinaryReader(_stream);

			this.FilePath = filePath;
			this.Load();
		}

		public void Close()
		{
			if (_stream != null)
				_stream.Dispose();

			if (_br != null)
				_br.Dispose();
		}

		public void Load()
		{
			_stream.Position = _stream.Length - 0x18;

			this.Footer = new IpfFooter();
			this.Footer.FileCount = _br.ReadUInt16();
			var fileTableOffset = _br.ReadUInt32();
			_br.ReadUInt16();
			_br.ReadUInt32();
			_br.ReadUInt32(); // compression
			this.Footer.PatchVersion = _br.ReadUInt32();
			this.Footer.NewVersion = _br.ReadUInt32();

			_stream.Position = fileTableOffset;

			this.Files = new List<IpfFile>();
			for (int i = 0; i < this.Footer.FileCount; i++)
			{
				var ipfFile = new IpfFile(this);

				var pathLength = _br.ReadUInt16();
				_br.ReadUInt32(); // crc32
				ipfFile.SizeCompressed = _br.ReadUInt32();
				ipfFile.SizeUncompressed = _br.ReadUInt32();
				ipfFile.Offset = _br.ReadUInt32();

				var length = _br.ReadUInt16();
				ipfFile.PackFileName = new string(_br.ReadChars(length));

				var path = new string(_br.ReadChars(pathLength));
				ipfFile.Path = path.Replace("\\", "/");
				ipfFile.FullPath = Path.Combine(ipfFile.PackFileName, path).Replace("\\", "/");

				this.Files.Add(ipfFile);
			}
		}

		public byte[] ReadData(long offset, int length)
		{
			byte[] data;

			lock (_extractLock)
			{
				_stream.Position = offset;
				data = _br.ReadBytes(length);
			}

			return data;
		}
	}

	public class IpfFile
	{
		private readonly string[] _noCompression = new[] { ".jpg", ".jpg", ".fsb", ".mp3" };

		public Ipf Ipf { get; set; }
		public string PackFileName { get; set; }
		public string Path { get; set; }
		public string FullPath { get; set; }
		public uint Offset { get; set; }
		public uint SizeCompressed { get; set; }
		public uint SizeUncompressed { get; set; }

		public IpfFile(Ipf ipf)
		{
			this.Ipf = ipf;
		}

		public byte[] GetData()
		{
			var ext = System.IO.Path.GetExtension(this.Path);
			var data = this.Ipf.ReadData(this.Offset, (int)this.SizeCompressed);

			if (_noCompression.Contains(ext.ToLowerInvariant()))
				return data;

			return this.Decompress(data);
		}

		private byte[] Decompress(byte[] data)
		{
			if (this.Ipf.Footer.NewVersion > 11000 || this.Ipf.Footer.NewVersion == 0)
			{
				var pkw = new PkwareTraditionalEncryptionData("ofO1a0ueXA? [\xFFs h %?");
				data = pkw.Decrypt(data, data.Length);
			}

			using (var msOut = new MemoryStream())
			using (var msIn = new MemoryStream(data))
			using (var deflate = new DeflateStream(msIn, CompressionMode.Decompress))
			{
				deflate.CopyTo(msOut);
				return msOut.ToArray();
			}
		}
	}

	public class IpfFooter
	{
		public ushort FileCount { get; set; }
		public uint NewVersion { get; set; }
		public uint PatchVersion { get; set; }
	}
}

