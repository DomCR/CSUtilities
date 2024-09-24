using Xunit;

namespace CSMath.Tests
{
	public class QuaternionTests
	{
		[Fact]
		public void CreateFromYawPitchRollTest()
		{
			XYZ xyz = new XYZ(Utilities.DegToRad(90), 0, 0);
			Quaternion q = Quaternion.CreateFromYawPitchRoll(xyz);
		}
	}
}
