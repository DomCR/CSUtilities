using System;
using CSUtilities.Converters;
using Xunit;

namespace CSUtilities.Tests.Converters;

public class EndianConverterCrossTests
{
	private readonly BigEndianConverter _big = new BigEndianConverter();

	private readonly LittleEndianConverter _little = new LittleEndianConverter();

	[Fact]
	public void BigAndLittle_ProduceReversedBytes_Int()
	{
		int value = 0x01020304;
		var bigBytes = _big.GetBytes(value);
		var littleBytes = _little.GetBytes(value);

		Array.Reverse(littleBytes);
		Assert.Equal(bigBytes, littleBytes);
	}

	[Fact]
	public void BigAndLittle_ProduceReversedBytes_Long()
	{
		long value = 0x0102030405060708;
		var bigBytes = _big.GetBytes(value);
		var littleBytes = _little.GetBytes(value);

		Array.Reverse(littleBytes);
		Assert.Equal(bigBytes, littleBytes);
	}

	[Fact]
	public void BigAndLittle_ProduceReversedBytes_Short()
	{
		short value = 0x0102;
		var bigBytes = _big.GetBytes(value);
		var littleBytes = _little.GetBytes(value);

		Array.Reverse(littleBytes);
		Assert.Equal(bigBytes, littleBytes);
	}

	[Fact]
	public void BigAndLittle_ProduceReversedBytes_UInt()
	{
		uint value = 0xAABBCCDD;
		var bigBytes = _big.GetBytes(value);
		var littleBytes = _little.GetBytes(value);

		Array.Reverse(littleBytes);
		Assert.Equal(bigBytes, littleBytes);
	}

	[Fact]
	public void BigEndianBytes_ReadByLittleEndian_WithReverse()
	{
		int value = 12345;
		var bigBytes = _big.GetBytes(value);
		Array.Reverse(bigBytes);
		var result = _little.ToInt32(bigBytes);
		Assert.Equal(value, result);
	}

	[Fact]
	public void GetBytes_SameLength_AllTypes()
	{
		Assert.Equal(_big.GetBytes((short)1).Length, _little.GetBytes((short)1).Length);
		Assert.Equal(_big.GetBytes(1).Length, _little.GetBytes(1).Length);
		Assert.Equal(_big.GetBytes(1L).Length, _little.GetBytes(1L).Length);
		Assert.Equal(_big.GetBytes(1.0).Length, _little.GetBytes(1.0).Length);
		Assert.Equal(_big.GetBytes(1.0f).Length, _little.GetBytes(1.0f).Length);
		Assert.Equal(_big.GetBytes('A').Length, _little.GetBytes('A').Length);
	}

	[Fact]
	public void LittleEndianBytes_ReadByBigEndian_WithReverse()
	{
		int value = 12345;
		var littleBytes = _little.GetBytes(value);
		Array.Reverse(littleBytes);
		var result = _big.ToInt32(littleBytes);
		Assert.Equal(value, result);
	}
}