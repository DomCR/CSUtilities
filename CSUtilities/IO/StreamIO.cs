using CSUtilities.Converters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CSUtilities.IO
{
	internal class StreamIO : IDisposable
	{
		public long Position
		{
			get => this.m_stream.Position;
			set => this.m_stream.Position = value;
		}
		public long Length => this.m_stream.Length;
		private Stream m_stream = null;
		public StreamIO(Stream stream)
		{
			this.m_stream = stream;
		}
		//*******************************************************************
		public virtual byte[] ReadBytes(int length)
		{
			byte[] buffer = new byte[length];
			if (this.m_stream.Read(buffer, 0, length) < length)
				throw new EndOfStreamException();

			return buffer;
		}

		public int ReadInt()
		{
			return ReadInt<DefaultEndianConverter>();
		}
		public int ReadInt<T>() where T : IEndianConverter, new()
		{
			T converter = new T();

			byte[] buffer = new byte[4];
			this.m_stream.Read(buffer, 0, 4);
			return converter.ToInt32(buffer);
		}
		public double ReadDouble()
		{
			return ReadDouble<DefaultEndianConverter>();
		}
		public double ReadDouble<T>() where T : IEndianConverter, new()
		{
			T converter = new T();

			byte[] buffer = new byte[8];
			this.m_stream.Read(buffer, 0, 8);
			return converter.ToDouble(buffer);
		}
		public uint ReadUInt()
		{
			return ReadUInt<DefaultEndianConverter>();
		}
		public uint ReadUInt<T>() where T : IEndianConverter, new()
		{
			T converter = new T();

			byte[] buffer = new byte[4];
			this.m_stream.Read(buffer, 0, 4);
			return converter.ToUInt32(buffer);
		}
		/// <inheritdoc/>
		public void Dispose()
		{
			m_stream.Dispose();
		}
	}
}
