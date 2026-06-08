using System;

namespace CSMath.Geometry;

public struct Line3D : ILine<XYZ>, IEquatable<Line3D>
{
	/// <inheritdoc/>
	public XYZ Direction { get; set; }

	/// <inheritdoc/>
	public XYZ Origin { get; set; }

	/// <summary>
	/// Initialize a new instance of the <see cref="Line3D" /> class.
	/// </summary>
	/// <param name="origin">Origin point on the line.</param>
	/// <param name="direction">Line direction, must be a none zero vector.</param>
	/// <exception cref="ArgumentException"></exception>
	public Line3D(XYZ origin, XYZ direction)
	{
		this.Origin = origin;

		if (direction.Equals(XYZ.Zero))
		{
			throw new ArgumentException("The direction vector of the line cannot be a zero vector.");
		}

		this.Direction = direction.Normalize();
	}

	/// <summary>
	/// Creates a line from 2 points, the first point is the origin and the second point is used to calculate the direction.
	/// </summary>
	/// <param name="pt1">The first point, which will be the origin of the line.</param>
	/// <param name="pt2">The second point, used to calculate the direction of the line.</param>
	/// <returns>A new instance of the <see cref="Line3D"/> class.</returns>
	public static Line3D FromPoints(XYZ pt1, XYZ pt2)
	{
		return new Line3D(pt1, pt2 - pt1);
	}

	/// <summary>
	/// Creates a line from a segment, the origin of the line will be the origin of the segment and the direction will be the direction of the segment.
	/// </summary>
	/// <param name="segment">The 3D segment used to create the line.</param>
	/// <returns>A new instance of the <see cref="Line3D"/> class.</returns>
	public static Line3D FromSegment3D(Segment3D segment)
	{
		return new Line3D(segment.Origin, segment.Direction);
	}

	/// <inheritdoc/>
	public bool Equals(Line3D other)
	{
		return this.IsPointOnLine(other.Origin) && other.Direction == this.Direction;
	}

	/// <inheritdoc/>
	public XYZ FindIntersection(ILine<XYZ> line)
	{
		var point0 = Origin;
		var u = Direction;
		var point1 = line.Origin;
		var v = line.Direction;

		var w0 = point0 - point1;
		var a = u.Dot(u);
		var b = u.Dot(v);
		var c = v.Dot(v);
		var d = u.Dot(w0);
		var e = v.Dot(w0);

		var sc = ((b * e) - (c * d)) / ((a * c) - (b * b));
		var tc = ((a * e) - (b * d)) / ((a * c) - (b * b));

		var pt1 = point0 + (sc * u);
		var pt2 = point1 + (tc * v);

		if (pt1.Equals(pt2))
		{
			return pt1;
		}
		else
		{
			return XYZ.NaN;
		}
	}
}