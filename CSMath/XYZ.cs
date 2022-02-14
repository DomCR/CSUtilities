namespace CSMath
{
	public partial struct XYZ : IVector<XYZ>
	{
		public readonly static XYZ Zero = new XYZ(0, 0, 0);
		public readonly static XYZ AxisX = new XYZ(1, 0, 0);
		public readonly static XYZ AxisY = new XYZ(0, 1, 0);
		public readonly static XYZ AxisZ = new XYZ(0, 0, 1);

		public double X { get; set; }
		public double Y { get; set; }
		public double Z { get; set; }

		public XYZ(double x, double y, double z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		/// <summary>
		/// Constructs a vector whose elements are all the single specified value.
		/// </summary>
		/// <param name="value">The element to fill the vector with.</param>
		public XYZ(double value) : this(value, value, value) { }

		public XYZ(double[] components) : this(components[0], components[1], components[2]) { }

		/// <summary>
		/// Computes the cross product of two coordinates.
		/// </summary>
		/// <param name="xyz1">The first coordinate.</param>
		/// <param name="xyz2">The second coordinate.</param>
		/// <returns>The cross product.</returns>
		public static XYZ Cross(XYZ xyz1, XYZ xyz2)
		{
			return new XYZ(
				xyz1.Y * xyz2.Z - xyz1.Z * xyz2.Y,
				xyz1.Z * xyz2.X - xyz1.X * xyz2.Z,
				xyz1.X * xyz2.Y - xyz1.Y * xyz2.X);
		}

		public static XYZ FindNormal(XYZ point1, XYZ point2, XYZ point3)
		{
			XYZ a = point2.Substract(point1);
			XYZ b = point3.Substract(point1);

			// N = Cross(a, b)
			XYZ n = XYZ.Cross(a, b);
			XYZ normal = n.Normalize();

			return normal;
		}

		/// <inheritdoc/>
		public double[] GetComponents()
		{
			return new double[] { X, Y, Z };
		}

		/// <inheritdoc/>
		public XYZ SetComponents(double[] components)
		{
			return new XYZ(components);
		}

		/// <inheritdoc/>
		public override bool Equals(object obj)
		{
			if (!(obj is XYZ other))
				return false;

			return this.X == other.X && this.Y == other.Y && this.Z == other.Z;
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode();
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			return $"{X},{Y},{Z}";
		}
	}
}
