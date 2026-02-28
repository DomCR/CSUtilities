using CSUtilities.Converters;
using Xunit;

namespace CSUtilities.Tests.Converters;

public class LittleEndianConverterTests : BaseEndianConverterTests<LittleEndianConverter>
{
	[Fact]
	public void GetBytes_Int_LittleEndianByteOrder()
	{
		int value = 0x01020304;
		var bytes = converter.GetBytes(value);
		Assert.Equal(new byte[] { 0x04, 0x03, 0x02, 0x01 }, bytes);
	}

	[Fact]
	public void ToChar_WithOffset()
	{
		char value = 'A';
		var raw = converter.GetBytes(value);
		byte[] bytes = new byte[raw.Length + 2];
		bytes[0] = 0xFF;
		raw.CopyTo(bytes, 1);
		bytes[bytes.Length - 1] = 0xAA;

		var result = converter.ToChar(bytes, 1);
		Assert.Equal(value, result);
	}

	[Fact]
	public void ToDouble_WithOffset()
	{
		double value = 1.5;
		var raw = converter.GetBytes(value);
		byte[] bytes = new byte[raw.Length + 2];
		bytes[0] = 0xFF;
		raw.CopyTo(bytes, 1);
		bytes[bytes.Length - 1] = 0xAA;

		var result = converter.ToDouble(bytes, 1);
		Assert.Equal(value, result);
	}

	[Fact]
	public void ToInt16_WithOffset()
	{
		byte[] bytes = { 0xFF, 0x01, 0x02, 0xFF };
		var result = converter.ToInt16(bytes, 1);
		Assert.Equal(0x0201, result);
	}

	[Fact]
	public void ToInt32_WithOffset()
	{
		byte[] bytes = { 0xFF, 0x00, 0x00, 0x00, 0x01, 0xFF };
		var result = converter.ToInt32(bytes, 1);
		Assert.Equal(0x1000000, result);
	}

	[Fact]
	public void ToInt64_WithOffset()
	{
		byte[] bytes = { 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01, 0xAA };
		var result = converter.ToInt64(bytes, 1);
		Assert.Equal(0x100000000000000, result);
	}

	[Fact]
	public void ToSingle_WithOffset()
	{
		float value = 1.5f;
		var raw = converter.GetBytes(value);
		byte[] bytes = new byte[raw.Length + 2];
		bytes[0] = 0xFF;
		raw.CopyTo(bytes, 1);
		bytes[bytes.Length - 1] = 0xAA;

		var result = converter.ToSingle(bytes, 1);
		Assert.Equal(value, result);
	}

	[Fact]
	public void ToUInt16_WithOffset()
	{
		byte[] bytes = { 0xFF, 0x00, 0x0A, 0xFF };
		var result = converter.ToUInt16(bytes, 1);
		Assert.Equal((ushort)0xA00, result);
	}

	[Fact]
	public void ToUInt32_WithOffset()
	{
		byte[] bytes = { 0xAA, 0x00, 0x00, 0x00, 0x0F, 0xBB };
		var result = converter.ToUInt32(bytes, 1);
		Assert.Equal((uint)0xF000000, result);
	}

	[Fact]
	public void ToUInt64_WithOffset()
	{
		byte[] bytes = { 0xFF, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x05, 0xCC };
		var result = converter.ToUInt64(bytes, 1);
		Assert.Equal(0x500000000000000UL, result);
	}
}