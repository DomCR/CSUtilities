using System;

namespace CSMath.Geometry;

/// <summary>
/// Represents a 2D line defined by an origin point and a direction vector.
/// </summary>
public struct Line2D : ILine<XY>, IEquatable<Line2D>
{
	/// <inheritdoc/>
	public XY Direction { get; set; }

	/// <summary>
	/// Gets the y-intercept of the line, calculated from the origin and slope.
	/// </summary>
	public double Offset { get { return Origin.Y - this.Slope * Origin.X; } }

	/// <inheritdoc/>
	public XY Origin { get; set; }

	/// <summary>
	/// Gets the slope of the line based on its direction vector.
	/// </summary>
	public double Slope { get { return this.Direction.Y / this.Direction.X; } }

	public Line2D(XY origin, XY direction)
	{
		this.Origin = origin;
		this.Direction = direction;
	}

	/// <inheritdoc/>
	public bool Equals(Line2D other)
	{
		return this.IsPointOnLine(other.Origin) && other.Direction == this.Direction;
	}

	/// <inheritdoc/>
	public override bool Equals(object obj)
	{
		return obj is Line2D && Equals((Line2D)obj);
	}

	/// <inheritdoc/>
	public XY FindIntersection(ILine<XY> line)
	{
		if (this.Direction.IsParallel(line.Direction))
		{
			return XY.NaN;
		}

		XY v = line.Origin - this.Origin;
		double cross = XY.Cross(this.Direction, line.Direction);
		double s = (v.X * line.Direction.Y - v.Y * line.Direction.X) / cross;
		return this.Origin + s * this.Direction;
	}

	/// <summary>
	/// Determines if a given point lies on the line.
	/// </summary>
	/// <param name="lambda">The parameter value along the line.</param>
	/// <returns>The point on the line corresponding to the given parameter value.</returns>
	public XY PointInLine(double lambda)
	{
		return this.Origin + lambda * this.Direction;
	}

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		return this.Origin.GetHashCode() ^ this.Direction.GetHashCode();
	}
}