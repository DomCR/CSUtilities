using CSUtilities.Converters;
using System;
using Xunit;

namespace CSUtilities.Tests.Converters;

public abstract class BaseEndianConverterTests<T>
	where T : IEndianConverter, new()
{
	protected readonly T converter = new T();

	[Fact]
	public void GetBytes_Char_RoundTrip()
	{
		char value = 'Z';
		var bytes = converter.GetBytes(value);
		var result = converter.ToChar(bytes);
		Assert.Equal(value, result);
	}

	[Fact]
	public void GetBytes_Double_RoundTrip()
	{
		double value = 3.141592653589793;
		var bytes = converter.GetBytes(value);
		var result = converter.ToDouble(bytes);
		Assert.Equal(value, result);
	}

	[Fact]
	public void GetBytes_Float_RoundTrip()
	{
		float value = 2.71828f;
		var bytes = converter.GetBytes(value);
		var result = converter.ToSingle(bytes);
		Assert.Equal(value, result);
	}

	[Fact]
	public void GetBytes_Generic_Int()
	{
		int value = 42;
		var bytes = converter.GetBytes(value);
		var genericBytes = converter.GetBytes<int>(value);
		Assert.Equal(bytes, genericBytes);
	}

	[Theory]
	[InlineData(int.MinValue)]
	[InlineData(int.MaxValue)]
	[InlineData(0)]
	[InlineData(-1)]
	public void GetBytes_Int_BoundaryValues(int value)
	{
		var bytes = converter.GetBytes(value);
		var result = converter.ToInt32(bytes);
		Assert.Equal(value, result);
	}

	[Fact]
	public void GetBytes_Int_RoundTrip()
	{
		int value = 123456789;
		var bytes = converter.GetBytes(value);
		var result = converter.ToInt32(bytes);
		Assert.Equal(value, result);
	}

	[Theory]
	[InlineData(long.MinValue)]
	[InlineData(long.MaxValue)]
	[InlineData(0L)]
	[InlineData(-1L)]
	public void GetBytes_Long_BoundaryValues(long value)
	{
		var bytes = converter.GetBytes(value);
		var result = converter.ToInt64(bytes);
		Assert.Equal(value, result);
	}

	[Fact]
	public void GetBytes_Long_RoundTrip()
	{
		long value = 1234567890123456789;
		var bytes = converter.GetBytes(value);
		var result = converter.ToInt64(bytes);
		Assert.Equal(value, result);
	}

	[Theory]
	[InlineData(short.MinValue)]
	[InlineData(short.MaxValue)]
	[InlineData((short)0)]
	[InlineData((short)-1)]
	public void GetBytes_Short_BoundaryValues(short value)
	{
		var bytes = converter.GetBytes(value);
		var result = converter.ToInt16(bytes);
		Assert.Equal(value, result);
	}

	[Fact]
	public void GetBytes_Short_RoundTrip()
	{
		short value = 12345;
		var bytes = converter.GetBytes(value);
		var result = converter.ToInt16(bytes);
		Assert.Equal(value, result);
	}

	[Fact]
	public void GetBytes_UInt_RoundTrip()
	{
		uint value = 3000000000;
		var bytes = converter.GetBytes(value);
		var result = converter.ToUInt32(bytes);
		Assert.Equal(value, result);
	}

	[Fact]
	public void GetBytes_ULong_RoundTrip()
	{
		ulong value = 9876543210987654321;
		var bytes = converter.GetBytes(value);
		var result = converter.ToUInt64(bytes);
		Assert.Equal(value, result);
	}

	[Fact]
	public void GetBytes_UShort_RoundTrip()
	{
		ushort value = 54321;
		var bytes = converter.GetBytes(value);
		var result = converter.ToUInt16(bytes);
		Assert.Equal(value, result);
	}
}
