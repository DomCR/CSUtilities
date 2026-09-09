using CSMath.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace CSMath.Geometry;

/// <summary>
/// Represents a 2D circular arc defined by a center point, radius, and angular range.
/// </summary>
public struct Arc2D
{
	/// <summary>
	/// Gets or sets the center point of the arc.
	/// </summary>
	public XY Center { get; set; }

	/// <summary>
	/// Gets or sets the ending angle of the arc in radians.
	/// </summary>
	public double EndAngle { get; set; }

	/// <summary>
	/// Gets or sets the radius of the arc.
	/// </summary>
	public double Radius { get; set; }

	/// <summary>
	/// Gets or sets the starting angle of the arc in radians.
	/// </summary>
	public double StartAngle { get; set; }

	public Arc2D(XY center, double radius, double startAngle, double endAngle)
	{
		this.Center = center;
		this.Radius = radius;
		this.StartAngle = startAngle;
		this.EndAngle = endAngle;
	}

	/// <summary>
	/// Calculates the intersection points between this arc and a line segment.
	/// </summary>
	/// <param name="line">The line segment to test for intersection.</param>
	/// <param name="precision">The precision value for the calculation.</param>
	/// <returns>An enumerable collection of intersection points. Returns an empty collection if no intersections exist.</returns>
	public IEnumerable<XY> FindIntersections(Line2D line, double precision = MathHelper.Epsilon)
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
			if (this.InAngularRange(tangentPoint))
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
		if (this.InAngularRange(firstPoint))
		{
			intersections.Add(firstPoint);
		}

		XY secondPoint = new XY((baseX - offsetX) * invLengthSquared, (baseY - offsetY) * invLengthSquared) + this.Center;
		if (this.InAngularRange(secondPoint))
		{
			intersections.Add(secondPoint);
		}

		return intersections;
	}

	/// <summary>
	/// Determines whether a point lies within the angular range of this arc.
	/// </summary>
	/// <param name="point">The point to test.</param>
	/// <returns><c>true</c> if the point's angle from the center falls within the arc's angular range; otherwise, <c>false</c>.</returns>
	public bool InAngularRange(XY point)
	{
		XY dir = point - this.Center;
		double angle = System.Math.Atan2(dir.Y, dir.X);
		return MathHelper.IsAngleInRange(angle, this.StartAngle, this.EndAngle);
	}
}