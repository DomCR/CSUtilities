using CSUtilities.Converters;
using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CSUtilities.IO;

/// <summary>
/// Utility class to read different data from a stream.
/// </summary>
internal class StreamIO : IDisposable
{
	/// <summary>
	/// Gets or sets the position within the current stream.
	/// </summary>
	public virtual long Position
	{
		get => this._stream.Position;
		set => this._stream.Position = value;
	}

	/// <summary>
	/// Gets the length in bytes of the stream.
	/// </summary>
	public virtual long Length => this._stream.Length;

	public Encoding Encoding { get; set; } = Encoding.Default;

	public IEndianConverter EndianConverter { get; set; } = new DefaultEndianConverter();

	public Stream Stream { get { return _stream; } }

	protected Stream _stream = null;

	/// <summary>
	/// Initializes a new instance of the <see cref="StreamIO" /> class.
	/// </summary>
	/// <param name="filename">File to read/write.</param>
	public StreamIO(string filename) : this(filename, FileMode.Open, FileAccess.ReadWrite) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="StreamIO" /> class.
	/// </summary>
	/// <param name="filename">File to read/write.</param>
	/// <param name="mode"></param>
	/// <param name="access"></param>
	public StreamIO(string filename, FileMode mode, FileAccess access)
	{
		_stream = (Stream)File.Open(filename, mode, access);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="StreamIO" /> class.
	/// </summary>
	/// <param name="stream"></param>
	/// <param name="createCopy"></param>
	/// <param name="resetPosition"></param>
	public StreamIO(Stream stream, bool createCopy, bool resetPosition)
	{
		if (stream == null)
		{
			throw new ArgumentNullException(nameof(stream));
		}

		//Readers require seeking. A non-seekable input therefore has to be buffered from its
		//current position; querying Position or Length first would itself throw.
		if (!stream.CanSeek || createCopy)
		{
			MemoryStream copy = new MemoryStream();
			if (stream.CanSeek)
			{
				long position = stream.Position;
				try
				{
					stream.Position = 0;
					stream.CopyTo(copy);
				}
				finally
				{
					stream.Position = position;
				}
			}
			else
			{
				stream.CopyTo(copy);
			}

			copy.Position = 0;
			_stream = copy;
		}
		else
		{
			this._stream = stream;
		}

		if (resetPosition)
		{
			//Reset the position to the begining
			_stream.Position = 0L;
		}
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="StreamIO" /> class.
	/// </summary>
	public StreamIO(Stream stream, bool createCopy) : this(stream, createCopy, false) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="StreamIO" /> class.
	/// </summary>
	public StreamIO(Stream stream) : this(stream, false, false) { }

	/// <summary>
	/// Initializes a new instance of the <see cref="StreamIO" /> class.
	/// </summary>
	public StreamIO(byte[] arr) : this(new MemoryStream(arr)) { }

	/// <summary>
	/// Get an array of bytes given an offset, before the operation the position is set to 0.
	/// </summary>
	/// <param name="offset"></param>
	/// <param name="length"></param>
	/// <returns></returns>
	/// <remarks>This operation don't advance the positon.</remarks>
	/// <exception cref="ArgumentOutOfRangeException"></exception>
	public byte[] GetBytes(int offset, int length)
	{
		if (length < 0)
			throw new ArgumentOutOfRangeException("Length cannot be negative.");

		//Save the current position
		long save = this.Position;
		//Set the position to the begining
		this.Position = offset;

		try
		{
			return this.ReadBytes(length);
		}
		finally
		{
			//A failed read must not leave this utility at the requested offset.
			this.Position = save;
		}
	}

	public async Task<byte[]> GetBytesAsync(int offset, int length)
	{
		if (length < 0)
			throw new ArgumentOutOfRangeException("Length cannot be negative.");

		//Save the current position
		long save = this.Position;
		//Set the position to the begining
		this.Position = offset;

		try
		{
			return await this.ReadBytesAsync(length);
		}
		finally
		{
			this.Position = save;
		}
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
		long position = this.Position;
		try
		{
			return this.ReadBytes(count);
		}
		finally
		{
			this.Position = position;
		}
	}

	/// <summary>
	/// Read a single byte form the stream.
	/// </summary>
	/// <returns></returns>
	public virtual byte ReadByte()
	{
		byte[] arr = new byte[1];
		byte b = _stream.Read(arr, 0, 1) == 1 ?
			arr[0] : throw new EndOfStreamException();

		return b;
	}

	public virtual async Task<byte> ReadByteAsync(CancellationToken cancellationToken = default)
	{
		byte[] arr = new byte[1];
		byte b = await _stream.ReadAsync(arr, 0, 1, cancellationToken) == 1 ?
			arr[0] : throw new EndOfStreamException();

		return b;
	}

	/// <summary>
	/// Read a character from the stream
	/// </summary>
	/// <returns></returns>
	public char ReadChar()
	{
		return (char)this.ReadByte();
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

		int offset = 0;
		while (offset < length)
		{
			int read = this._stream.Read(buffer, offset, length - offset);
			if (read == 0)
			{
				throw new EndOfStreamException();
			}

			offset += read;
		}

		return buffer;
	}

	public virtual async Task<byte[]> ReadBytesAsync(int length, CancellationToken cancellationToken = default)
	{
		if (length < 0)
			throw new ArgumentOutOfRangeException("Length cannot be negative.");

		byte[] buffer = new byte[length];

		int offset = 0;
		while (offset < length)
		{
			int read = await this._stream.ReadAsync(buffer, offset, length - offset, cancellationToken);
			if (read == 0)
			{
				throw new EndOfStreamException();
			}

			offset += read;
		}

		return buffer;
	}

	/// <summary>
	/// Read the stream as a string until it finds the match character
	/// </summary>
	/// <param name="match">Character to match</param>
	/// <returns></returns>
	public string ReadUntil(char match)
	{
		string line = string.Empty;
		char last;

		do
		{
			last = this.ReadChar();
			line += last;

		} while (match != last);

		return line;
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
	/// Read a <see cref="long"/> value form the stream.
	/// </summary>
	/// <returns></returns>
	public long ReadLong()
	{
		return ReadLong<DefaultEndianConverter>();
	}

	/// <summary>
	/// Read a <see cref="long"/> value form the stream.
	/// </summary>
	/// <typeparam name="T">Endian converter to process the bytes.</typeparam>
	/// <returns></returns>
	public long ReadLong<T>() where T : IEndianConverter, new()
	{
		T converter = new T();

		byte[] buffer = this.ReadBytes(8);
		return converter.ToInt64(buffer);
	}

	/// <summary>
	/// Read a <see cref="ulong"/> value form the stream.
	/// </summary>
	/// <returns></returns>
	public ulong ReadULong()
	{
		return ReadULong<DefaultEndianConverter>();
	}

	/// <summary>
	/// Read a <see cref="ulong"/> value form the stream.
	/// </summary>
	/// <typeparam name="T">Endian converter to process the bytes.</typeparam>
	/// <returns></returns>
	public ulong ReadULong<T>() where T : IEndianConverter, new()
	{
		T converter = new T();

		byte[] buffer = this.ReadBytes(8);
		return converter.ToUInt64(buffer);
	}

	/// <summary>
	/// Read a string from the stream using the default encoding.
	/// </summary>
	/// <param name="length"></param>
	/// <returns></returns>
	public string ReadString(int length)
	{
		return ReadString(length, this.Encoding);
	}

	/// <summary>
	/// Read a string from the stream.
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

	/// <summary>
	/// Write a value as an array of bytes
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="value"></param>
	public void Write<T>(T value)
		where T : struct
	{
		this.Write(value, new DefaultEndianConverter());
	}

	public void Write<T, E>(T value)
		where T : struct
		where E : IEndianConverter, new()
	{
		this.Write(value, new E());
	}

	/// <summary>
	/// Write a value as an array of bytes defining the byte order
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="value"></param>
	/// <param name="converter"></param>
	public void Write<T>(T value, IEndianConverter converter)
		where T : struct
	{
		byte[] arr = converter.GetBytes(value);
		this._stream.Write(arr, 0, arr.Length);
	}

	public virtual void WriteBytes(byte[] buffer)
	{
		this._stream.Write(buffer, 0, buffer.Length);
	}

	public virtual void WriteBytes(byte[] buffer, int offset, int count)
	{
		this._stream.Write(buffer, offset, count);
	}

	/// <summary>
	/// Write a string using the default encoding
	/// </summary>
	/// <param name="value"></param>
	public void Write(string value)
	{
		this.Write(value, this.Encoding);
	}

	/// <summary>
	/// Write a string with an specific encoding
	/// </summary>
	/// <param name="value"></param>
	/// <param name="encoding"></param>
	public void Write(string value, Encoding encoding)
	{
		byte[] arr = encoding.GetBytes(value);
		this._stream.Write(arr, 0, arr.Length);
	}

	/// <inheritdoc/>
	public void Dispose()
	{
		_stream.Dispose();
	}
}
