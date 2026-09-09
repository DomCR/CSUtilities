using CSMath.Extensions;
using Xunit;

namespace CSMath.Tests.Extensions;

public class IntExtensionsTests
{
	[Theory]
	[InlineData(0, true)]
	[InlineData(2, true)]
	[InlineData(-2, true)]
	[InlineData(1, false)]
	[InlineData(-1, false)]
	[InlineData(int.MaxValue, false)]
	[InlineData(int.MinValue, true)]
	public void IsEven_ReturnsExpected(int value, bool expected)
	{
		Assert.Equal(expected, value.IsEven());
	}

	[Theory]
	[InlineData(0, false)]
	[InlineData(1, false)]
	[InlineData(-1, true)]
	[InlineData(int.MaxValue, false)]
	[InlineData(int.MinValue, true)]
	public void IsNegative_ReturnsExpected(int value, bool expected)
	{
		Assert.Equal(expected, value.IsNegative());
	}

	[Theory]
	[InlineData(0, false)]
	[InlineData(2, false)]
	[InlineData(-2, false)]
	[InlineData(1, true)]
	[InlineData(-1, true)]
	[InlineData(int.MaxValue, true)]
	[InlineData(int.MinValue, false)]
	public void IsOdd_ReturnsExpected(int value, bool expected)
	{
		Assert.Equal(expected, value.IsOdd());
	}
}
