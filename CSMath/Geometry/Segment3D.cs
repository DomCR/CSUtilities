using System;

namespace CSMath.Geometry;

public struct Segment3D : ILine<XYZ>, IEquatable<Segment3D>
{
	/// <inheritdoc/>
	public XYZ Direction { get { return this.End - this.Origin; } }

	/// <summary>
	/// Gets or sets the end point of the segment in 3D space.
	/// </summary>
	public XYZ End { get; set; }

	/// <inheritdoc/>
	public XYZ Origin { get; set; }

	public Segment3D(XYZ origin, XYZ end)
	{
		this.Origin = origin;
		this.End = end;
	}

	/// <inheritdoc/>
	public bool Equals(Segment3D other)
	{
		return this.Origin.Equals(other.Origin) && this.End.Equals(other.End);
	}

	/// <inheritdoc/>
	public override bool Equals(object obj)
	{
		return obj is Segment3D && Equals((Segment3D)obj);
	}

	/// <summary>
	/// Calculates the intersection point of the current line segment and the specified line.
	/// </summary>
	/// <param name="line">The line to intersect with.</param>
	/// <returns>The intersection point as an XYZ object, or XYZ.NaN if there is no intersection within the bounds of the current
	/// segment.</returns>
	public XYZ FindIntersection(ILine<XYZ> line)
	{
		var curr = new Line3D(this.Origin, this.Direction);
		var intersection = curr.FindIntersection(line);

		if (intersection.IsNaN())
		{
			return XYZ.NaN;
		}

		if (intersection.X < Math.Min(this.Origin.X, this.End.X)
			|| intersection.X > Math.Max(this.Origin.X, this.End.X)
			|| intersection.Y < Math.Min(this.Origin.Y, this.End.Y)
			|| intersection.Y > Math.Max(this.Origin.Y, this.End.Y))
		{
			return XYZ.NaN;
		}

		return intersection;
	}

	/// <inheritdoc/>
	public override int GetHashCode()
	{
		return this.Origin.GetHashCode() ^ this.End.GetHashCode();
	}
}