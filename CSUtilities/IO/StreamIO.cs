using CSUtilities.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSUtilities.IO
{
	/// <summary>
	/// Utility class to read different data from a string
	/// </summary>
	internal class StreamIO : IDisposable
	{
		/// <summary>
		/// Gets or sets the position within the current stream.
		/// </summary>
		public long Position
		{
			get => this.m_stream.Position;
			set => this.m_stream.Position = value;
		}
		/// <summary>
		/// Gets the length in bytes of the stream.
		/// </summary>
		public long Length => this.m_stream.Length;
		private Stream m_stream = null;
		public StreamIO(Stream stream)
		{
			this.m_stream = stream;
		}
		//*******************************************************************
		/// <summary>
		/// Look into a byte without moving the position of the stream.
		/// </summary>
		/// <returns></returns>
		public byte LookByte()
		{
			byte b = LookBytes(1)[0];
			return b;
		}
		/// <summary>
		/// Look into an array of bytes without moving the position of the stream.
		/// </summary>
		/// <param name="count"></param>
		/// <returns></returns>
		public byte[] LookBytes(int count)
		{
			byte[] bs = this.ReadBytes(count);
			this.Position -= count;
			return bs;
		}
		/// <summary>
		/// Reads a byte from the stream and advances the position within the stream by one byte, or returns -1 if at the end of the stream.
		/// </summary>
		/// <returns></returns>
		public byte ReadByte()
		{
			return (byte)m_stream.ReadByte();
		}
		public virtual byte[] ReadBytes(int length)
		{
			byte[] buffer = new byte[length];
			if (this.m_stream.Read(buffer, 0, length) < length)
				throw new EndOfStreamException();

			return buffer;
		}
		public short ReadShort()
		{
			return ReadShort<DefaultEndianConverter>();
		}
		public short ReadShort<T>() where T : IEndianConverter, new()
		{
			T converter = new T();

			byte[] buffer = this.ReadBytes(2);
			return converter.ToInt16(buffer);
		}
		public int ReadInt()
		{
			return ReadInt<DefaultEndianConverter>();
		}
		public int ReadInt<T>() where T : IEndianConverter, new()
		{
			T converter = new T();

			byte[] buffer = this.ReadBytes(4);
			return converter.ToInt32(buffer);
		}
		public uint ReadUInt()
		{
			return ReadUInt<DefaultEndianConverter>();
		}
		public uint ReadUInt<T>() where T : IEndianConverter, new()
		{
			T converter = new T();

			byte[] buffer = this.ReadBytes(4);
			return converter.ToUInt32(buffer);
		}
		public double ReadDouble()
		{
			return ReadDouble<DefaultEndianConverter>();
		}
		public double ReadDouble<T>() where T : IEndianConverter, new()
		{
			T converter = new T();

			byte[] buffer = this.ReadBytes(8);
			return converter.ToDouble(buffer);
		}
		/// <inheritdoc/>
		public void Dispose()
		{
			m_stream.Dispose();
		}
	}
}
