using Xunit;

namespace CSMath.Tests;

public class Matrix4Tests
{
	[Fact]
	public void GetArbitraryAxisReturnsIdentityForTheZAxis()
	{
		Assert.Equal(Matrix4.Identity, Matrix4.GetArbitraryAxis(XYZ.AxisZ));
	}

	[Fact]
	public void GetArbitraryAxisMirrorsXAndZForANegatedZAxis()
	{
		Matrix4 m = Matrix4.GetArbitraryAxis(-XYZ.AxisZ);

		AssertUtils.AreEqual(new XYZ(-1, 2, -3), m * new XYZ(1, 2, 3));
	}

	[Fact]
	public void GetArbitraryAxisHandlesANormalThatIsNearlyButNotExactlyTheZAxis()
	{
		//What a real drawing stores: a normal that means "+Z" but carries the dust of whatever
		//arithmetic produced it. The arbitrary axis algorithm switches to the Y reference below a
		//1/64 threshold precisely for this case - written as integer division that threshold is
		//zero, the branch never runs, and the cross product with Z is then so close to the zero
		//vector that normalising it yields a direction unrelated to the entity.
		XYZ almostZ = new XYZ(-3.707652143685886E-13, 8.818399612355268E-14, 1);

		Matrix4 m = Matrix4.GetArbitraryAxis(almostZ);

		XYZ point = new XYZ(3952258.943164733, -9072802.879860947, 0);

		AssertUtils.AreEqual(point, m * point);
	}

	[Fact]
	public void GetArbitraryAxisUsesTheYReferenceBelowTheThreshold()
	{
		//Just inside the threshold the X axis comes from Y x N, just outside it from Z x N. Both
		//have to produce a right handed frame whose Z is the normal itself.
		foreach (double tilt in new[] { 1.0 / 128.0, 1.0 / 32.0 })
		{
			XYZ normal = new XYZ(tilt, 0, 1).Normalize();

			Matrix4 m = Matrix4.GetArbitraryAxis(normal);

			AssertUtils.AreEqual(normal, m * XYZ.AxisZ);
		}
	}
}
