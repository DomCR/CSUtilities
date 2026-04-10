using System;
using Xunit;

namespace CSMath.Tests;

public class MathHelperTests
{
	[Theory]
	[InlineData(MathHelper.HalfPI, 0.0)]
	[InlineData(MathHelper.PI, -1.0)]
	[InlineData(MathHelper.TwoPI, 1.0)]
	[InlineData(0.0, 1.0)]
	public void Cos_ReturnsExpected(double input, double expected)
	{
		Assert.Equal(expected, MathHelper.Cos(input));
	}

	[Theory]
	[InlineData(0)]
	[InlineData(45)]
	[InlineData(90)]
	[InlineData(180)]
	public void DegToGrad_GradToDeg_Roundtrip(double degrees)
	{
		double gradians = MathHelper.DegToGrad(degrees);
		double result = MathHelper.GradToDeg(gradians);
		Assert.True(MathHelper.IsEqual(degrees, result),
			$"Expected {degrees} but got {result}");
	}

	[Theory]
	[InlineData(0, 0)]
	[InlineData(90, 100)]
	[InlineData(180, 200)]
	[InlineData(360, 400)]
	public void DegToGrad_ReturnsExpected(double degrees, double expectedGradians)
	{
		Assert.Equal(expectedGradians, MathHelper.DegToGrad(degrees), 10);
	}

	[Fact]
	public void DegToGradFactor_IsCorrect()
	{
		Assert.Equal(10.0 / 9.0, MathHelper.DegToGradFactor);
	}

	[Theory]
	[InlineData(0)]
	[InlineData(45)]
	[InlineData(90)]
	[InlineData(135)]
	[InlineData(180)]
	[InlineData(270)]
	public void DegToRad_RadToDeg_Roundtrip(double degrees)
	{
		double radians = MathHelper.DegToRad(degrees);
		double result = MathHelper.RadToDeg(radians);
		Assert.True(MathHelper.IsEqual(degrees, result),
			$"Expected {degrees} but got {result}");
	}

	[Theory]
	[InlineData(0, 0)]
	[InlineData(90, Math.PI / 2)]
	[InlineData(180, Math.PI)]
	[InlineData(360, Math.PI * 2)]
	public void DegToRad_ReturnsExpected(double degrees, double expectedRadians)
	{
		Assert.Equal(expectedRadians, MathHelper.DegToRad(degrees), 10);
	}

	[Fact]
	public void DegToRadFactor_IsCorrect()
	{
		Assert.Equal(Math.PI / 180.0, MathHelper.DegToRadFactor);
	}

	[Theory]
	[InlineData(0.05, 0.1, 0.0)]
	[InlineData(0.5, 0.1, 0.5)]
	public void FixZero_CustomThreshold_ReturnsExpected(double value, double threshold, double expected)
	{
		Assert.Equal(expected, MathHelper.FixZero(value, threshold));
	}

	[Theory]
	[InlineData(5e-13, 0.0)]
	[InlineData(5.0, 5.0)]
	public void FixZero_DefaultThreshold_ReturnsExpected(double value, double expected)
	{
		Assert.Equal(expected, MathHelper.FixZero(value));
	}

	[Theory]
	[InlineData(0, 0)]
	[InlineData(100, 90)]
	[InlineData(200, 180)]
	[InlineData(400, 360)]
	public void GradToDeg_ReturnsExpected(double gradians, double expectedDegrees)
	{
		Assert.Equal(expectedDegrees, MathHelper.GradToDeg(gradians), 10);
	}

	[Fact]
	public void GradToDegFactor_IsCorrect()
	{
		Assert.Equal(9.0 / 10.0, MathHelper.GradToDegFactor);
	}

	[Theory]
	[InlineData(0, 0)]
	[InlineData(100, Math.PI / 2)]
	[InlineData(200, Math.PI)]
	[InlineData(400, Math.PI * 2)]
	public void GradToRad_ReturnsExpected(double gradians, double expectedRadians)
	{
		Assert.Equal(expectedRadians, MathHelper.GradToRad(gradians), 10);
	}

	[Fact]
	public void GradToRadFactor_IsCorrect()
	{
		Assert.Equal(Math.PI / 200.0, MathHelper.GradToRadFactor);
	}

	[Fact]
	public void HalfPI_IsCorrect()
	{
		Assert.Equal(Math.PI / 2.0, MathHelper.HalfPI);
	}

	[Theory]
	[InlineData(0.0, true)]
	[InlineData(5e-13, true)]
	[InlineData(-5e-13, true)]
	[InlineData(1e-12, false)]
	[InlineData(-1e-12, false)]
	[InlineData(1.0, false)]
	[InlineData(-1.0, false)]
	public void IsAlmostZero_ReturnsExpected(double value, bool expected)
	{
		Assert.Equal(expected, MathHelper.IsAlmostZero(value));
	}

	[Theory]
	[InlineData(1.0, 1.0000000000005, true)]
	[InlineData(5.0, 5.0, true)]
	[InlineData(-5.0, 5.0, true)]
	[InlineData(5.0, 6.0, false)]
	public void IsEqual_ReturnsExpected(double a, double b, bool expected)
	{
		Assert.Equal(expected, MathHelper.IsEqual(a, b));
	}

	[Theory]
	[InlineData(1.0, 1.05, 0.1, true)]
	[InlineData(1.0, 1.5, 0.1, false)]
	public void IsEqual_CustomThreshold_ReturnsExpected(double a, double b, double threshold, bool expected)
	{
		Assert.Equal(expected, MathHelper.IsEqual(a, b, threshold));
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
		Assert.Equal(expected, MathHelper.IsZero(value));
	}

	[Theory]
	[InlineData(0.05, 0.1, true)]
	[InlineData(0.2, 0.1, false)]
	public void IsZero_CustomThreshold_ReturnsExpected(double value, double threshold, bool expected)
	{
		Assert.Equal(expected, MathHelper.IsZero(value, threshold));
	}

	[Theory]
	[InlineData(180, 180)]
	[InlineData(360, 360)]
	[InlineData(-360, 360)]
	[InlineData(720, 0)]
	[InlineData(450.0, 90.0)]
	[InlineData(-90.0, 270.0)]
	[InlineData(-450.0, 270.0)]
	[InlineData(0.0, 0.0)]
	public void NormalizeAngleTest(double number, double expected)
	{
		Assert.Equal(expected, MathHelper.NormalizeAngle(number));
	}

	[Theory]
	[InlineData(0.0, 0.0)]
	[InlineData(MathHelper.HalfPI, 90.0)]
	[InlineData(MathHelper.PI, 180.0)]
	[InlineData(MathHelper.TwoPI, 360.0)]
	public void RadToDeg_ReturnsExpected(double radians, double expected)
	{
		Assert.Equal(expected, MathHelper.RadToDeg(radians), 10);
	}

	[Fact]
	public void RadToDegFactor_IsCorrect()
	{
		Assert.Equal(180.0 / Math.PI, MathHelper.RadToDegFactor);
	}

	[Theory]
	[InlineData(MathHelper.HalfPI, 100.0)]
	[InlineData(MathHelper.PI, 200.0)]
	public void RadToGrad_ReturnsExpected(double radians, double expected)
	{
		Assert.Equal(expected, MathHelper.RadToGrad(radians), 10);
	}

	[Fact]
	public void RadToGradFactor_IsCorrect()
	{
		Assert.Equal(200.0 / Math.PI, MathHelper.RadToGradFactor);
	}

	[Theory]
	[InlineData(7.0, 5.0, 5.0)]
	[InlineData(8.0, 5.0, 10.0)]
	[InlineData(12.0, 5.0, 10.0)]
	[InlineData(13.0, 5.0, 15.0)]
	[InlineData(0.0, 5.0, 0.0)]
	[InlineData(-8.0, 5.0, -10.0)]
	public void RoundToNearest_ReturnsExpected(double number, double roundTo, double expected)
	{
		Assert.Equal(expected, MathHelper.RoundToNearest(number, roundTo));
	}

	[Theory]
	[InlineData(0.0, 0.0)]
	[InlineData(MathHelper.HalfPI, 1.0)]
	[InlineData(MathHelper.PI, 0.0)]
	[InlineData(MathHelper.ThreeHalfPI, -1.0)]
	public void Sin_ReturnsExpected(double input, double expected)
	{
		Assert.Equal(expected, MathHelper.Sin(input));
	}

	[Fact]
	public void ThreeHalfPI_IsCorrect()
	{
		Assert.Equal(3.0 * Math.PI * 0.5, MathHelper.ThreeHalfPI);
	}

	[Fact]
	public void TwoPI_IsCorrect()
	{
		Assert.Equal(Math.PI * 2.0, MathHelper.TwoPI);
	}
}