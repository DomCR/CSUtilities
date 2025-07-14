using System;

namespace CSMath.Geometry
{
	public struct Line2D : ILine<XY>, IEquatable<Line2D>
	{
		/// <inheritdoc/>
		public XY Direction { get; set; }

		public double Offset { get { return Origin.Y - this.Slope * Origin.X; } }

		/// <inheritdoc/>
		public XY Origin { get; set; }

		public double Slope { get { return (this.Direction.Y - this.Direction.Y) / (this.Direction.X - this.Direction.X); } }

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
	}
}