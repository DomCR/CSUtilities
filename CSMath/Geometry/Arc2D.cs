using System.Collections.Generic;
using System.Linq;

namespace CSMath.Geometry;

public struct Arc2D
{
	public double StartAngle { get; set; }

	public double EndAngle { get; set; }

	public double Radius { get; set; }

	public XY Center { get; set; }

	public IEnumerable<XY> GetIntersections(Line2D line, double precision)
	{
		double lengthSquared = line.Direction.GetLengthSquared();
		XY originOffset = line.Origin - this.Center;
		XY offsetEnd = originOffset + line.Direction;
		double crossProduct = originOffset.X * offsetEnd.Y - offsetEnd.X * originOffset.Y;
		double discriminant = this.Radius * this.Radius * lengthSquared - crossProduct * crossProduct;

		if (discriminant < 0)
		{
			return Enumerable.Empty<XY>();
		}

		double invLengthSquared = 1.0 / lengthSquared;
		double baseX = crossProduct * line.Direction.Y;
		double baseY = -crossProduct * line.Direction.X;

		if (discriminant <= 0)
		{
			XY tangentPoint = new XY(baseX * invLengthSquared, baseY * invLengthSquared) + this.Center;
			if (this.ContainsAngleProjection(tangentPoint))
			{
				return new[] { tangentPoint };
			}

			return Enumerable.Empty<XY>();
		}

		double sqrtDiscriminant = System.Math.Sqrt(discriminant);
		double sign = line.Direction.Y < 0.0 ? -1.0 : 1.0;
		double offsetX = sign * line.Direction.X * sqrtDiscriminant;
		double offsetY = System.Math.Abs(line.Direction.Y) * sqrtDiscriminant;

		List<XY> intersections = new List<XY>(2);
		XY firstPoint = new XY((baseX + offsetX) * invLengthSquared, (baseY + offsetY) * invLengthSquared) + this.Center;
		if (this.ContainsAngleProjection(firstPoint))
		{
			intersections.Add(firstPoint);
		}

		XY secondPoint = new XY((baseX - offsetX) * invLengthSquared, (baseY - offsetY) * invLengthSquared) + this.Center;
		if (this.ContainsAngleProjection(secondPoint))
		{
			intersections.Add(secondPoint);
		}

		return intersections;
	}

	public bool ContainsAngleProjection(XY point)
	{
		XY dir = point - this.Center;
		double angle = System.Math.Atan2(dir.Y, dir.X);
		return MathHelper.IsAngleInRange(angle, this.StartAngle, this.EndAngle);
	}
}