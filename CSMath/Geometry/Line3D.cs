using System;

namespace CSMath.Geometry
{
	// Eq: pt = origin + a * Direction
	public struct Line3D : ILine<XYZ>
	{
		/// <inheritdoc/>
		public XYZ Origin { get; set; }

		/// <inheritdoc/>
		public XYZ Direction { get; set; }

		/// <summary>
		/// Initialize a new 
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

		public XYZ FindIntersection(Line3D other)
		{
			var point0 = Origin;
			var u = Direction;
			var point1 = other.Origin;
			var v = other.Direction;

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
}