using Xunit;

namespace CSMath.Tests
{
	public class QuaternionTests
	{
		[Fact]
		public void CreateFromYawPitchRollTest()
		{
			XYZ xyz = new XYZ(90, 0, 0);
			var q = Quaternion.CreateFromYawPitchRoll(MathUtils.ToRadian(xyz));
		}
	}
}
