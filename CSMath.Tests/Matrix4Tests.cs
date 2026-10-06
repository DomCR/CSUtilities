using CSMath.Extensions;
using Xunit;

namespace CSMath.Tests;

public class Matrix4Tests
{
	[Fact]
	public void GetArbitraryAxisNearZUsesWorldYTest()
	{
		//Normal close to +Z: |X| and |Y| are below 1/64, so the arbitrary axis algorithm uses Wy x N
		XYZ normal = new XYZ(0.01, 0, 1).Normalize();
		XYZ expectedX = XYZ.Cross(XYZ.AxisY, normal).Normalize();

		Matrix4 expected = Matrix4.GetArbitraryAxis(expectedX, normal);

		Assert.Equal(expected, Matrix4.GetArbitraryAxis(normal));
	}

	[Fact]
	public void GetArbitraryAxisUsesWorldZTest()
	{
		XYZ normal = new XYZ(1, 1, 1).Normalize();
		XYZ expectedX = XYZ.Cross(XYZ.AxisZ, normal).Normalize();

		Matrix4 expected = Matrix4.GetArbitraryAxis(expectedX, normal);

		Assert.Equal(expected, Matrix4.GetArbitraryAxis(normal));
	}
}
