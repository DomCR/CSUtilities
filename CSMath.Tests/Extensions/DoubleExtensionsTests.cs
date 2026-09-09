using CSMath.Extensions;
using Xunit;

namespace CSMath.Tests.Extensions;

public class DoubleExtensionsTests
{
	[Theory]
	[InlineData(0.05, 0.1, true)]
	[InlineData(0.2, 0.1, false)]
	public void IsZero_CustomThreshold_ReturnsExpected(double value, double threshold, bool expected)
	{
		Assert.Equal(expected, value.IsZero(threshold));
	}

	[Theory]
	[InlineData(0.0, true)]
	[InlineData(5e-13, true)]
	[InlineData(-5e-13, true)]
	[InlineData(1e-12, true)]
	[InlineData(-1e-12, true)]
	[InlineData(1.0, false)]
	[InlineData(-1.0, false)]
	public void IsZero_ReturnsExpected(double value, bool expected)
	{
		Assert.Equal(expected, value.IsZero());
	}
}