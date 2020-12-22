using CSUtilities.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSUtilities.IO
{
	/// <summary>
	/// Utility class to read different data from a stream.
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
		public StreamIO(byte[] arr) : this(new MemoryStream(arr)) { }
		//*******************************************************************
		/// <summary>
		/// Get an array of bytes given an offset, before the operation the position is set to 0.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="length"></param>
		/// <returns></returns>
		/// <remarks>This operation don't advance the positon.</remarks>
		public byte[] GetBytes(int offset, int length)
		{
			if (length < 0)
				throw new ArgumentOutOfRangeException("Length cannot be negative.");

			byte[] buffer = new byte[length];

			//Save the current position
			long save = this.Position;
			//Set the position to the begining
			this.Position = offset;

			buffer = this.ReadBytes( length);
			//if (this.m_stream.Read(buffer, offset, length) < length)
			//	throw new EndOfStreamException();

			//Reset the position
			this.Position = save;

			return buffer;
		}
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
		/// <summary>
		/// Read n bytes at the stream position.
		/// </summary>
		/// <remarks>
		/// Override this method to change the reading system of the whole the class.
		/// </remarks>
		/// <param name="length"></param>
		/// <returns></returns>
		public virtual byte[] ReadBytes(int length)
		{
			if (length < 0)
				throw new ArgumentOutOfRangeException("Length cannot be negative.");

			byte[] buffer = new byte[length];

			if (this.m_stream.Read(buffer, 0, length) < length)
				throw new EndOfStreamException();

			return buffer;
		}
		/// <summary>
		/// Read a <see cref="short"/> value form the stream.
		/// </summary>
		/// <returns></returns>
		public short ReadShort()
		{
			return ReadShort<DefaultEndianConverter>();
		}
		/// <summary>
		/// Read a <see cref="short"/> value form the stream.
		/// </summary>
		/// <typeparam name="T">Endian converter to process the bytes.</typeparam>
		/// <returns></returns>
		public short ReadShort<T>() where T : IEndianConverter, new()
		{
			T converter = new T();

			byte[] buffer = this.ReadBytes(2);
			return converter.ToInt16(buffer);
		}
		/// <summary>
		/// Read a <see cref="ushort"/> value form the stream.
		/// </summary>
		/// <returns></returns>
		public ushort ReadUShort()
		{
			return ReadUShort<DefaultEndianConverter>();
		}
		/// <summary>
		/// Read a <see cref="ushort"/> value form the stream.
		/// </summary>
		/// <typeparam name="T">Endian converter to process the bytes.</typeparam>
		/// <returns></returns>
		public ushort ReadUShort<T>() where T : IEndianConverter, new()
		{
			T converter = new T();

			byte[] buffer = this.ReadBytes(2);
			return converter.ToUInt16(buffer);
		}
		/// <summary>
		/// Read a <see cref="int"/> value form the stream.
		/// </summary>
		/// <returns></returns>
		public int ReadInt()
		{
			return ReadInt<DefaultEndianConverter>();
		}
		/// <summary>
		/// Read a <see cref="int"/> value form the stream.
		/// </summary>
		/// <typeparam name="T">Endian converter to process the bytes.</typeparam>
		/// <returns></returns>
		public int ReadInt<T>() where T : IEndianConverter, new()
		{
			T converter = new T();

			byte[] buffer = this.ReadBytes(4);
			return converter.ToInt32(buffer);
		}
		/// <summary>
		/// Read a <see cref="uint"/> value form the stream.
		/// </summary>
		/// <returns></returns>
		public uint ReadUInt()
		{
			return ReadUInt<DefaultEndianConverter>();
		}
		/// <summary>
		/// Read a <see cref="uint"/> value form the stream.
		/// </summary>
		/// <typeparam name="T">Endian converter to process the bytes.</typeparam>
		/// <returns></returns>
		public uint ReadUInt<T>() where T : IEndianConverter, new()
		{
			T converter = new T();

			byte[] buffer = this.ReadBytes(4);
			return converter.ToUInt32(buffer);
		}
		/// <summary>
		/// Read a <see cref="float"/> value form the stream.
		/// </summary>
		/// <returns></returns>
		public float ReadSingle()
		{
			return ReadSingle<DefaultEndianConverter>();
		}
		/// <summary>
		/// Read a <see cref="float"/> value form the stream.
		/// </summary>
		/// <typeparam name="T">Endian converter to process the bytes.</typeparam>
		/// <returns></returns>
		public float ReadSingle<T>() where T : IEndianConverter, new()
		{
			T converter = new T();

			byte[] buffer = this.ReadBytes(4);
			return converter.ToSingle(buffer);
		}
		/// <summary>
		/// Read a <see cref="double"/> value form the stream.
		/// </summary>
		/// <returns></returns>
		public double ReadDouble()
		{
			return ReadDouble<DefaultEndianConverter>();
		}
		/// <summary>
		/// Read a <see cref="double"/> value form the stream.
		/// </summary>
		/// <typeparam name="T">Endian converter to process the bytes.</typeparam>
		/// <returns></returns>
		public double ReadDouble<T>() where T : IEndianConverter, new()
		{
			T converter = new T();

			byte[] buffer = this.ReadBytes(8);
			return converter.ToDouble(buffer);
		}
		/// <summary>
		/// Read a string from the stream using the default encoding.
		/// </summary>
		/// <param name="length"></param>
		/// <returns></returns>
		public string ReadString(int length)
		{
			return ReadString(length, Encoding.Default);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="length"></param>
		/// <param name="encoding"></param>
		/// <returns></returns>
		public string ReadString(int length, Encoding encoding)
		{
			if (length == 0)
				return string.Empty;

			byte[] numArray = this.ReadBytes(length);
			return encoding.GetString(numArray);
		}
		/// <inheritdoc/>
		public void Dispose()
		{
			m_stream.Dispose();
		}
	}
}
