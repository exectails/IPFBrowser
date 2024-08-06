using System;
using System.IO;
using System.Text;

namespace IPFBrowser
{
    /// <summary>
    /// Extensions for BinaryReader and BinaryWriter to make working with
    /// IES files easier.
    /// </summary>
    public static class BinaryReaderWriterExtensions
    {
        private const int XorKey = 1;

        /// <summary>
        /// XORs buffer and returns it as a string in the given encoding.
        /// </summary>
        /// <param name="buffer">Buffer to XOR.</param>
        private static void XorBuffer(ref byte[] buffer)
        {
            for (var i = 0; i < buffer.Length; ++i)
            {
                if (buffer[i] == 0)
                    break;

                buffer[i] = (byte)(buffer[i] ^ XorKey);
            }
        }

        /// <summary>
        /// Reads ushort-length-prefixed, XORed UTF8 string from reader and
        /// returns it.
        /// </summary>
        /// <param name="reader"></param>
        /// <returns></returns>
        public static string ReadXoredLpString(this BinaryReader reader)
        {
            var length = reader.ReadUInt16();
            return ReadXoredFixedString(reader, length);
        }

        /// <summary>
        /// Reads XORed UTF8 string with given length from reader and
        /// returns it.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ReadXoredFixedString(this BinaryReader reader, int length)
        {
            if (length <= 0)
                return "";

            var buffer = reader.ReadBytes(length);
            XorBuffer(ref buffer);
            var index = Array.IndexOf(buffer, (byte)0);

            if (index != -1)
                length = index;

            return Encoding.UTF8.GetString(buffer, 0, length);
        }

        /// <summary>
        /// Reads XORed UTF8 string with given length from reader and
        /// returns it.
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string ReadFixedString(this BinaryReader reader, int length)
        {
            if (length <= 0)
                return "";

            var buffer = reader.ReadBytes(length);
            var index = Array.IndexOf(buffer, (byte)0);

            if (index != -1)
                length = index;

            return Encoding.UTF8.GetString(buffer, 0, length);
        }

        /// <summary>
        /// Writes UTF8 string to the writer, prefixed with a length.
        /// The string is XORed.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="length"></param>
        public static void WriteXoredLpString(this BinaryWriter writer, string value)
        {
            var length = Encoding.UTF8.GetByteCount(value);
            writer.Write((ushort)length);
            WriteXoredFixedString(writer, value, length);
        }

        /// <summary>
        /// Writes UTF8 string with a fixed length to the writer. String
        /// is cut off if it's to long and extended with null bytes if
        /// too short. The actual string is XORed.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="length"></param>
        public static void WriteXoredFixedString(this BinaryWriter writer, string value, int length)
        {
            var buffer = Encoding.UTF8.GetBytes(value);
            var writeLength = (buffer.Length > length ? length : buffer.Length);

            XorBuffer(ref buffer);

            writer.Write(buffer, 0, writeLength);

            for (; writeLength < length; ++writeLength)
                writer.Write((byte)0);
        }

        /// <summary>
        /// Writes UTF8 string with a fixed length to the writer. String
        /// is cut off if it's to long and extended with null bytes if
        /// too short.
        /// </summary>
        /// <param name="writer"></param>
        /// <param name="value"></param>
        /// <param name="length"></param>
        public static void WriteFixedString(this BinaryWriter writer, string value, int length)
        {
            var buffer = Encoding.UTF8.GetBytes(value);
            var writeLength = (buffer.Length > length ? length : buffer.Length);

            writer.Write(buffer, 0, writeLength);

            for (; writeLength < length; ++writeLength)
                writer.Write((byte)0);
        }
    }
}