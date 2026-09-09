using CSMath.Extensions;
using System;

namespace CSMath.Geometry;

public struct Segment2D : ILine<XY>, IEquatable<Segment2D>
{
	/// <inheritdoc/>
	public XY Direction { get { return this.End - this.Origin; } }

	/// <summary>
	/// Gets or sets the end point of the segment in the XY plane.
	/// </summary>
	public XY End { get; set; }

	/// <inheritdoc/>
	public XY Origin { get; set; }

	public Segment2D(XY origin, XY end)
	{
		this.Origin = origin;
		this.End = end;
	}

	/// <inheritdoc/>
	public bool Equals(Segment2D other)
	{
		return this.Origin.Equals(other.Origin) && this.End.Equals(other.End);
	}

	/// <inheritdoc/>
	public override bool Equals(object obj)
	{
		return obj is Segment2D && Equals((Segment2D)obj);
	}

	/// <summary>
	/// Calculates the intersection point of the current line segment and the specified line.
	/// </summary>
	/// <param name="line">The line to intersect with.</param>
	/// <returns>The intersection point as an XY object, or XY.NaN if there is no intersection within the bounds of the current
	/// segment.</returns>
	public XY FindIntersection(ILine<XY> line)
	{
		var curr = new Line2D(this.Origin, this.Direction);
		var intersection = curr.FindIntersection(line);

		if (intersection.IsNaN())
		{
			return XY.NaN;
		}

		if (intersection.X < Math.Min(this.Origin.X, this.End.X)
			|| intersection.X > Math.Max(this.Origin.X, this.End.X)
			|| intersection.Y < Math.Min(this.Origin.Y, this.End.Y)
			|| intersection.Y > Math.Max(this.Origin.Y, this.End.Y))
		{
			return XY.NaN;
		}

		return intersection;
	}

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		return this.Origin.GetHashCode() ^ this.End.GetHashCode();
	}
}