using CSMath.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace CSMath.Geometry;

/// <summary>
/// Represents a circle in 2D space defined by a center point and radius.
/// </summary>
public struct Circle2D
{
	/// <summary>
	/// Gets or sets the center point of the circle.
	/// </summary>
	public XY Center { get; set; }

	/// <summary>
	/// Gets or sets the radius of the circle.
	/// </summary>
	public double Radius { get; set; }

	/// <summary>
	/// Initializes a new instance of the <see cref="Circle2D"/> struct with a center point and radius.
	/// </summary>
	/// <param name="center">The center point of the circle.</param>
	/// <param name="radius">The radius of the circle.</param>
	public Circle2D(XY center, double radius)
	{
		this.Center = center;
		this.Radius = radius;
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="Circle2D"/> struct with center coordinates and radius.
	/// </summary>
	/// <param name="centerX">The X coordinate of the center point.</param>
	/// <param name="centerY">The Y coordinate of the center point.</param>
	/// <param name="radius">The radius of the circle.</param>
	public Circle2D(double centerX, double centerY, double radius)
	{
		this.Center = new XY(centerX, centerY);
		this.Radius = radius;
	}

	/// <summary>
	/// Finds the intersection points between this circle and a line.
	/// </summary>
	/// <param name="line">The line to test for intersection with this circle.</param>
	/// <returns>
	/// An enumerable collection of intersection points. Returns: <br/>
	/// - Empty enumerable if the line does not intersect the circle.<br/>
	/// - Single point if the line is tangent to the circle.<br/>
	/// - Two points if the line intersects the circle at two locations.<br/>
	/// </returns>
	public IEnumerable<XY> FindIntersections(Line2D line)
	{
		double lengthSquared = line.Direction.GetLengthSquared();
		XY relativeOrigin = line.Origin - this.Center;
		double determinant = XY.Cross(relativeOrigin, line.Direction);
		double discriminant = this.Radius * this.Radius * lengthSquared - determinant * determinant;

		if (discriminant.IsZero())
		{
			double x = determinant * line.Direction.Y / lengthSquared;
			double y = -determinant * line.Direction.X / lengthSquared;

			return new[]
			{
				new XY(x, y) + this.Center
			};
		}

		if (discriminant < 0.0)
		{
			return Enumerable.Empty<XY>();
		}

		double sqrtDiscriminant = System.Math.Sqrt(discriminant);
		double sign = line.Direction.Y < 0.0 ? -1.0 : 1.0;

		double baseX = determinant * line.Direction.Y;
		double baseY = -determinant * line.Direction.X;
		double offsetX = sign * line.Direction.X * sqrtDiscriminant;
		double offsetY = System.Math.Abs(line.Direction.Y) * sqrtDiscriminant;

		XY intersection1 = new XY(
			(baseX + offsetX) / lengthSquared,
			(baseY + offsetY) / lengthSquared) + this.Center;

		XY intersection2 = new XY(
			(baseX - offsetX) / lengthSquared,
			(baseY - offsetY) / lengthSquared) + this.Center;

		return new[]
		{
			intersection1,
			intersection2
		};
	}
}