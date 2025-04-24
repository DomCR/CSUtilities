using System;
using System.Collections.Generic;

namespace CSMath.Geometry
{
	public interface ICurve
	{
		List<XYZ> PolygonalVertexes(int precision);

		XYZ PolarCoordinateRelativeToCenter(double angle);
	}
}