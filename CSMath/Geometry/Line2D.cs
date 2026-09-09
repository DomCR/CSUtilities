using CSMath.Extensions;
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

	/// <summary>
	/// Creates a line from 2 points, the first point is the origin and the second point is used to calculate the direction.
	/// </summary>
	/// <param name="pt1">The first point, which will be the origin of the line.</param>
	/// <param name="pt2">The second point, used to calculate the direction of the line.</param>
	/// <returns>A new instance of the <see cref="Line2D"/> class.</returns>
	public static Line2D FromPoints(XY pt1, XY pt2)
	{
		return new Line2D(pt1, pt2 - pt1);
	}

	/// <summary>
	/// Creates a line from a 2D segment, using the segment's origin and direction.
	/// </summary>
	/// <param name="segment">The 2D segment used to create the line.</param>
	/// <returns>A new instance of the <see cref="Line2D"/> class.</returns>
	public static Line2D FromSegment2D(Segment2D segment)
	{
		return new Line2D(segment.Origin, segment.Direction);
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

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		return this.Origin.GetHashCode() ^ this.Direction.GetHashCode();
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
}