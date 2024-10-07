using Xunit;
using Xunit.Sdk;

namespace CSMath.Tests
{
	public class BoundingBoxTests
	{
		[Theory]
		[InlineData(0, 0, 0, true)]
		[InlineData(11, 0, 0, false)]
		[InlineData(11, 0, -11, false)]
		[InlineData(5, 0, 5, true)]
		[InlineData(10, 10, 10, true)]
		public void IsInTest(double x, double y, double z, bool isIn)
		{
			BoundingBox box = new BoundingBox(-10, -10, -10, 10, 10, 10);

			XYZ xyz = new XYZ(x, y, z);
			Assert.Equal(isIn, box.IsIn(xyz));
		}
	}
}
