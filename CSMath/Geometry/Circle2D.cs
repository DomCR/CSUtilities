using ACadSharp.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CSMath.Geometry;

public struct Circle2D
{
	public XY Center { get; set; }

	public double Radius { get; set; }

	public Circle2D(XY center, double radius)
	{
		this.Center = center;
		this.Radius = radius;
	}

	public Circle2D(double centerX, double centerY, double radius)
	{
		this.Center = new XY(centerX, centerY);
		this.Radius = radius;
	}

	public IEnumerable<XY> FindIntersections(Line2D line)
	{
		double lengthSquared = line.Direction.GetLengthSquared();
		if (MathHelper.IsZero(lengthSquared))
		{
			return Enumerable.Empty<XY>();
		}

		XY relativeOrigin = line.Origin - this.Center;
		double determinant = XY.Cross(relativeOrigin, line.Direction);
		double discriminant = this.Radius * this.Radius * lengthSquared - determinant * determinant;

		if (MathHelper.IsZero(discriminant))
		{
			double x = determinant * line.Direction.Y / lengthSquared;
			double y = -determinant * line.Direction.X / lengthSquared;

			return new[]
			{
				MathHelper.FixZero(new XY(x, y) + this.Center)
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
			MathHelper.FixZero(intersection1),
			MathHelper.FixZero(intersection2)
		};
	}
}