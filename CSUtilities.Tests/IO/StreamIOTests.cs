using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CSUtilities.IO;
using Xunit;

namespace CSUtilities.Tests.IO;

public class StreamIOTests : IDisposable
{
	private readonly MemoryStream _memoryStream;
	private readonly StreamIO _streamIO;

	public StreamIOTests()
	{
		_memoryStream = new MemoryStream();
		_streamIO = new StreamIO(_memoryStream);
	}

	public void Dispose()
	{
		_streamIO.Dispose();
		_memoryStream.Dispose();
	}

	[Fact]
	public void WriteAndReadByte_WorksCorrectly()
	{
		_streamIO.Write<byte>(0x42);
		_streamIO.Position = 0;
		Assert.Equal(0x42, _streamIO.ReadByte());
	}

	[Fact]
	public void WriteAndReadBytes_WorksCorrectly()
	{
		byte[] data = { 1, 2, 3, 4, 5 };
		_streamIO.WriteBytes(data);
		_streamIO.Position = 0;
		Assert.Equal(data, _streamIO.ReadBytes(data.Length));
	}

	[Fact]
	public void WriteAndReadString_WorksCorrectly()
	{
		string value = "Hello, world!";
		_streamIO.Write(value, Encoding.UTF8);
		_streamIO.Position = 0;
		string result = _streamIO.ReadString(value.Length, Encoding.UTF8);
		Assert.Equal(value, result);
	}

	[Fact]
	public void GetBytes_ReturnsCorrectBytes()
	{
		byte[] data = { 10, 20, 30, 40, 50 };
		_streamIO.WriteBytes(data);
		byte[] result = _streamIO.GetBytes(1, 3);
		Assert.Equal(new byte[] { 20, 30, 40 }, result);
	}

	[Fact]
	public async Task GetBytesAsync_ReturnsCorrectBytes()
	{
		byte[] data = { 100, 101, 102, 103, 104 };
		_streamIO.WriteBytes(data);
		byte[] result = await _streamIO.GetBytesAsync(2, 2);
		Assert.Equal(new byte[] { 102, 103 }, result);
	}

	[Fact]
	public void LookByte_DoesNotAdvancePosition()
	{
		_streamIO.Write<byte>(0x7F);
		_streamIO.Position = 0;
		byte b = _streamIO.LookByte();
		Assert.Equal(0x7F, b);
		Assert.Equal(0, _streamIO.Position);
	}

	[Fact]
	public void LookBytes_DoesNotAdvancePosition()
	{
		byte[] data = { 1, 2, 3, 4 };
		_streamIO.WriteBytes(data);
		_streamIO.Position = 0;
		byte[] looked = _streamIO.LookBytes(2);
		Assert.Equal(new byte[] { 1, 2 }, looked);
		Assert.Equal(0, _streamIO.Position);
	}

	[Fact]
	public void ReadChar_ReadsCorrectChar()
	{
		_streamIO.Write<byte>((byte)'A');
		_streamIO.Position = 0;
		Assert.Equal('A', _streamIO.ReadChar());
	}

	[Fact]
	public void ReadUntil_ReadsUntilMatch()
	{
		string value = "abc;def";
		_streamIO.Write(value, Encoding.ASCII);
		_streamIO.Position = 0;
		string result = _streamIO.ReadUntil(';');
		Assert.Equal("abc;", result);
	}

	[Fact]
	public void ReadShort_ReadsCorrectValue()
	{
		short value = 0x1234;
		_streamIO.Write(value);
		_streamIO.Position = 0;
		Assert.Equal(value, _streamIO.ReadShort());
	}

	[Fact]
	public void ReadInt_ReadsCorrectValue()
	{
		int value = 0x12345678;
		_streamIO.Write(value);
		_streamIO.Position = 0;
		Assert.Equal(value, _streamIO.ReadInt());
	}

	[Fact]
	public void ReadLong_ReadsCorrectValue()
	{
		long value = 0x123456789ABCDEF0;
		_streamIO.Write(value);
		_streamIO.Position = 0;
		Assert.Equal(value, _streamIO.ReadLong());
	}

	[Fact]
	public void ReadSingle_ReadsCorrectValue()
	{
		float value = 123.456f;
		_streamIO.Write(value);
		_streamIO.Position = 0;
		Assert.Equal(value, _streamIO.ReadSingle(), 3);
	}

	[Fact]
	public void ReadDouble_ReadsCorrectValue()
	{
		double value = 123456.789;
		_streamIO.Write(value);
		_streamIO.Position = 0;
		Assert.Equal(value, _streamIO.ReadDouble(), 6);
	}

	[Fact]
	public void ReadUShort_ReadsCorrectValue()
	{
		ushort value = 0xFEDC;
		_streamIO.Write(value);
		_streamIO.Position = 0;
		Assert.Equal(value, _streamIO.ReadUShort());
	}

	[Fact]
	public void ReadUInt_ReadsCorrectValue()
	{
		uint value = 0xDEADBEEF;
		_streamIO.Write(value);
		_streamIO.Position = 0;
		Assert.Equal(value, _streamIO.ReadUInt());
	}

	[Fact]
	public void ReadULong_ReadsCorrectValue()
	{
		ulong value = 0x123456789ABCDEF0UL;
		_streamIO.Write(value);
		_streamIO.Position = 0;
		Assert.Equal(value, _streamIO.ReadULong());
	}

	[Fact]
	public void WriteBytes_WithOffsetAndCount_WritesCorrectly()
	{
		byte[] data = { 1, 2, 3, 4, 5 };
		_streamIO.WriteBytes(data, 1, 3);
		_streamIO.Position = 0;
		Assert.Equal(new byte[] { 2, 3, 4 }, _streamIO.ReadBytes(3));
	}

	[Fact]
	public void ReadBytes_ThrowsOnNegativeLength()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => _streamIO.ReadBytes(-1));
	}

	[Fact]
	public void GetBytes_ThrowsOnNegativeLength()
	{
		Assert.Throws<ArgumentOutOfRangeException>(() => _streamIO.GetBytes(0, -1));
	}

	[Fact]
	public void ReadBytes_ThrowsOnEndOfStream()
	{
		Assert.Throws<EndOfStreamException>(() => _streamIO.ReadBytes(1));
	}
}