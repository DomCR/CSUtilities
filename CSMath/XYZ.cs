using System;

namespace CSMath
{
	public partial struct XYZ : IVector<XYZ>, IEquatable<XYZ>
	{
		public readonly static XYZ Zero = new XYZ(0, 0, 0);
		public readonly static XYZ AxisX = new XYZ(1, 0, 0);
		public readonly static XYZ AxisY = new XYZ(0, 1, 0);
		public readonly static XYZ AxisZ = new XYZ(0, 0, 1);

		/// <summary>
		/// Specifies the X-value of the vector component
		/// </summary>
		public double X { get; set; }

		/// <summary>
		/// Specifies the Y-value of the vector component
		/// </summary>
		public double Y { get; set; }

		/// <summary>
		/// Specifies the Z-value of the vector component
		/// </summary>
		public double Z { get; set; }

		/// <inheritdoc/>
		public uint Dimension { get { return 3; } }

		/// <inheritdoc/>
		public double this[uint index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return X;
					case 1:
						return Y;
					case 2:
						return Z;
					default:
						throw new IndexOutOfRangeException($"The index must be between 0 and {this.Dimension}.");
				}
			}
			set
			{
				switch (index)
				{
					case 0:
						X = value;
						break;
					case 1:
						Y = value;
						break;
					case 2:
						Z = value;
						break;
					default:
						throw new IndexOutOfRangeException($"The index must be between 0 and {this.Dimension}.");
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="x"></param>
		/// <param name="y"></param>
		/// <param name="z"></param>
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

		[Obsolete("Deprecated")]
		public XYZ(double[] components) : this(components[0], components[1], components[2]) { }

		public static XYZ CreateFrom(double[] arr)
		{
			return CreateFrom(arr, 0);
		}

		public static XYZ CreateFrom(double[] arr, int offset)
		{
			double[] values = new double[3];
			for (int i = offset; i < arr.Length && i < values.Length + offset; i++)
			{
				values[i] = (double)arr[i];
			}

			return new XYZ(values);
		}

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
			XYZ a = point2.Subtract(point1);
			XYZ b = point3.Subtract(point1);

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

			return this.Equals(other);
		}

		/// <summary>
		/// Indicates whether this instance and a specified object are equal with in a specific precison.
		/// </summary>
		/// <param name="other"></param>
		/// <param name="digits">number of decimals</param>
		/// <returns></returns>
		public bool Equals(XYZ other, int digits)
		{
			return other.IsEqual(this, digits);
		}

		/// <inheritdoc/>
		public bool Equals(XYZ other)
		{
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

		#region operators 

		/// <summary>
		/// Adds two vectors together.
		/// </summary>
		/// <param name="left">The first source vector.</param>
		/// <param name="right">The second source vector.</param>
		/// <returns>The summed vector.</returns>
		public static XYZ operator +(XYZ left, XYZ right)
		{
			return left.Add(right);
		}

		/// <summary>
		/// Subtracts the second vector from the first.
		/// </summary>
		/// <param name="left">The first source vector.</param>
		/// <param name="right">The second source vector.</param>
		/// <returns>The difference vector.</returns>
		public static XYZ operator -(XYZ left, XYZ right)
		{
			return left.Subtract(right);
		}

		/// <summary>
		/// Multiplies two vectors together.
		/// </summary>
		/// <param name="left">The first source vector.</param>
		/// <param name="right">The second source vector.</param>
		/// <returns>The product vector.</returns>
		public static XYZ operator *(XYZ left, XYZ right)
		{
			return left.Multiply(right);
		}

		/// <summary>
		/// Multiplies a vector by the given scalar.
		/// </summary>
		/// <param name="left">The source vector.</param>
		/// <param name="scalar">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		public static XYZ operator *(XYZ left, double scalar)
		{
			return left * new XYZ(scalar);
		}

		/// <summary>
		/// Multiplies a vector by the given scalar.
		/// </summary>
		/// <param name="scalar">The scalar value.</param>
		/// <param name="vector">The source vector.</param>
		/// <returns>The scaled vector.</returns>
		public static XYZ operator *(double scalar, XYZ vector)
		{
			return new XYZ(scalar) * vector;
		}

		/// <summary>
		/// Divides the first vector by the second.
		/// </summary>
		/// <param name="left">The first source vector.</param>
		/// <param name="right">The second source vector.</param>
		/// <returns>The vector resulting from the division.</returns>
		public static XYZ operator /(XYZ left, XYZ right)
		{
			return left.Divide(right);
		}

		/// <summary>
		/// Divides the vector by the given scalar.
		/// </summary>
		/// <param name="xyz">The source vector.</param>
		/// <param name="value">The scalar value.</param>
		/// <returns>The result of the division.</returns>
		public static XYZ operator /(XYZ xyz, float value)
		{
			float invDiv = 1.0f / value;

			return new XYZ(xyz.X * invDiv,
							xyz.Y * invDiv,
							xyz.Z * invDiv);
		}

		/// <summary>
		/// Divides the vector by the given scalar.
		/// </summary>
		/// <param name="xyz">The source vector.</param>
		/// <param name="value">The scalar value.</param>
		/// <returns>The result of the division.</returns>
		public static XYZ operator /(XYZ xyz, double value)
		{
			double invDiv = 1.0f / value;

			return new XYZ(xyz.X * invDiv,
							xyz.Y * invDiv,
							xyz.Z * invDiv);
		}

		/// <summary>
		/// Negates a given vector.
		/// </summary>
		/// <param name="value">The source vector.</param>
		/// <returns>The negated vector.</returns>
		public static XYZ operator -(XYZ value)
		{
			return Zero.Subtract(value);
		}

		/// <summary>
		/// Returns a boolean indicating whether the two given vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>True if the vectors are equal; False otherwise.</returns>
		public static bool operator ==(XYZ left, XYZ right)
		{
			return (left.X == right.X &&
					left.Y == right.Y &&
					left.Z == right.Z);
		}

		/// <summary>
		/// Returns a boolean indicating whether the two given vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>True if the vectors are not equal; False if they are equal.</returns>
		public static bool operator !=(XYZ left, XYZ right)
		{
			return (left.X != right.X ||
					left.Y != right.Y ||
					left.Z != right.Z);
		}

		public static explicit operator XYZ(XY xy)
		{
			return new XYZ(xy.X, xy.Y, 0);
		}

		#endregion
	}
}
