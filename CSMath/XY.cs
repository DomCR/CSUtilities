using System;

namespace CSMath
{
	public struct XY : IVector<XY>, IEquatable<XY>
	{
		public readonly static XY Zero = new XY(0, 0);
		public readonly static XY AxisX = new XY(1, 0);
		public readonly static XY AxisY = new XY(0, 1);

		/// <summary>
		/// Specifies the X-value of the vector component
		/// </summary>
		public double X { get; set; }

		/// <summary>
		/// Specifies the Y-value of the vector component
		/// </summary>
		public double Y { get; set; }

		/// <inheritdoc/>
		public uint Dimension { get { return 2; } }

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
					default:
						throw new IndexOutOfRangeException($"The index must be between 0 and {this.Dimension}.");
				}
			}
		}

		public XY(double x, double y)
		{
			X = x;
			Y = y;
		}

		/// <summary>
		/// Constructs a vector whose elements are all the single specified value.
		/// </summary>
		/// <param name="value">The element to fill the vector with.</param>
		public XY(double value) : this(value, value) { }

		[Obsolete("Deprecated")]
		public XY(double[] components) : this(components[0], components[1]) { }

		/// <summary>
		/// Get the angle
		/// </summary>
		/// <returns>angle in radians</returns>
		public double GetAngle()
		{
			return Math.Atan2(Y, X);
		}

		/// <inheritdoc/>
		public double[] GetComponents()
		{
			return new double[] { X, Y };
		}

		/// <inheritdoc/>
		public XY SetComponents(double[] components)
		{
			if (components.Length != this.Dimension)
			{
				throw new ArgumentOutOfRangeException(nameof(components), $"The components array must be formed by {this.Dimension} values");
			}

			return new XY(components[0], components[1]);
		}

		/// <inheritdoc/>
		public override bool Equals(object obj)
		{
			if (!(obj is XY other))
				return false;

			return this.Equals(other);
		}

		/// <summary>
		/// Indicates whether this instance and a specified object are equal with in a specific precison.
		/// </summary>
		/// <param name="other"></param>
		/// <param name="digits">number of decimals</param>
		/// <returns></returns>
		public bool Equals(XY other, int digits)
		{
			return other.IsEqual(this, digits);
		}

		/// <inheritdoc/>
		public bool Equals(XY other)
		{
			return this.X == other.X && this.Y == other.Y;
		}

		/// <inheritdoc/>
		public override int GetHashCode()
		{
			return this.X.GetHashCode() ^ this.Y.GetHashCode();
		}

		/// <inheritdoc/>
		public override string ToString()
		{
			return $"{X},{Y}";
		}

		/// <summary>
		/// Get the angle from 2 vectors
		/// </summary>
		/// <param name="u"></param>
		/// <param name="v"></param>
		/// <returns>angle in radians</returns>
		public static double GetAngle(XY u, XY v)
		{
			XY dir = v.Subtract(u);
			return dir.GetAngle();
		}

		#region operators 

		/// <summary>
		/// Adds two vectors together.
		/// </summary>
		/// <param name="left">The first source vector.</param>
		/// <param name="right">The second source vector.</param>
		/// <returns>The summed vector.</returns>
		public static XY operator +(XY left, XY right)
		{
			return left.Add(right);
		}

		/// <summary>
		/// Subtracts the second vector from the first.
		/// </summary>
		/// <param name="left">The first source vector.</param>
		/// <param name="right">The second source vector.</param>
		/// <returns>The difference vector.</returns>
		public static XY operator -(XY left, XY right)
		{
			return left.Subtract(right);
		}

		/// <summary>
		/// Multiplies two vectors together.
		/// </summary>
		/// <param name="left">The first source vector.</param>
		/// <param name="right">The second source vector.</param>
		/// <returns>The product vector.</returns>
		public static XY operator *(XY left, XY right)
		{
			return left.Multiply(right);
		}

		/// <summary>
		/// Multiplies a vector by the given scalar.
		/// </summary>
		/// <param name="left">The source vector.</param>
		/// <param name="scalar">The scalar value.</param>
		/// <returns>The scaled vector.</returns>
		public static XY operator *(XY left, double scalar)
		{
			return left * new XY(scalar);
		}

		/// <summary>
		/// Multiplies a vector by the given scalar.
		/// </summary>
		/// <param name="scalar">The scalar value.</param>
		/// <param name="vector">The source vector.</param>
		/// <returns>The scaled vector.</returns>
		public static XY operator *(double scalar, XY vector)
		{
			return new XY(scalar) * vector;
		}

		/// <summary>
		/// Divides the first vector by the second.
		/// </summary>
		/// <param name="left">The first source vector.</param>
		/// <param name="right">The second source vector.</param>
		/// <returns>The vector resulting from the division.</returns>
		public static XY operator /(XY left, XY right)
		{
			return left.Divide(right);
		}

		/// <summary>
		/// Divides the vector by the given scalar.
		/// </summary>
		/// <param name="XY">The source vector.</param>
		/// <param name="value">The scalar value.</param>
		/// <returns>The result of the division.</returns>
		public static XY operator /(XY XY, float value)
		{
			float invDiv = 1.0f / value;

			return new XY(XY.X * invDiv,
							XY.Y * invDiv);
		}

		/// <summary>
		/// Divides the vector by the given scalar.
		/// </summary>
		/// <param name="XY">The source vector.</param>
		/// <param name="value">The scalar value.</param>
		/// <returns>The result of the division.</returns>
		public static XY operator /(XY XY, double value)
		{
			double invDiv = 1.0f / value;

			return new XY(XY.X * invDiv,
							XY.Y * invDiv);
		}

		/// <summary>
		/// Negates a given vector.
		/// </summary>
		/// <param name="value">The source vector.</param>
		/// <returns>The negated vector.</returns>
		public static XY operator -(XY value)
		{
			return Zero.Subtract(value);
		}

		/// <summary>
		/// Returns a boolean indicating whether the two given vectors are equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>True if the vectors are equal; False otherwise.</returns>
		public static bool operator ==(XY left, XY right)
		{
			return (left.X == right.X &&
					left.Y == right.Y);
		}

		/// <summary>
		/// Returns a boolean indicating whether the two given vectors are not equal.
		/// </summary>
		/// <param name="left">The first vector to compare.</param>
		/// <param name="right">The second vector to compare.</param>
		/// <returns>True if the vectors are not equal; False if they are equal.</returns>
		public static bool operator !=(XY left, XY right)
		{
			return (left.X != right.X ||
					left.Y != right.Y);
		}

		public static explicit operator XY(XYZ xyz)
		{
			return new XY(xyz.X, xyz.Y);
		}

		#endregion
	}
}
