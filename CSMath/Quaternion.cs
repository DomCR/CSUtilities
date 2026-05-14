using System;

namespace CSMath;

/// <summary>
/// Four dimensional vector which is used to efficiently rotate an object about the (x,y,z) vector by the angle theta, where w = cos(theta/2).
/// </summary>
public partial struct Quaternion : IVector, IEquatable<Quaternion>
{
	/// <summary>
	/// Returns a Quaternion representing no rotation.
	/// </summary>
	public static Quaternion Identity
	{
		get { return new Quaternion(0, 0, 0, 1); }
	}

	/// <inheritdoc/>
	public uint Dimension { get { return 4; } }

	/// <summary>
	/// Specifies the rotation component of the Quaternion.
	/// </summary>
	public double W { get; set; }

	/// <summary>
	/// Specifies the X-value of the vector component of the Quaternion.
	/// </summary>
	public double X { get; set; }

	/// <summary>
	/// Specifies the Y-value of the vector component of the Quaternion.
	/// </summary>
	public double Y { get; set; }

	/// <summary>
	/// Specifies the Z-value of the vector component of the Quaternion.
	/// </summary>
	public double Z { get; set; }

	/// <summary>
	/// Constructs a Quaternion from the given components.
	/// </summary>
	/// <param name="x">The X component of the Quaternion.</param>
	/// <param name="y">The Y component of the Quaternion.</param>
	/// <param name="z">The Z component of the Quaternion.</param>
	/// <param name="w">The W component of the Quaternion.</param>
	public Quaternion(double x, double y, double z, double w)
	{
		this.X = x;
		this.Y = y;
		this.Z = z;
		this.W = w;
	}

	/// <summary>
	/// Constructs a Quaternion from the given vector and rotation parts.
	/// </summary>
	/// <param name="vector">The vector part of the Quaternion.</param>
	/// <param name="scalarPart">The rotation part of the Quaternion.</param>
	public Quaternion(XYZ vector, double scalarPart)
	{
		X = vector.X;
		Y = vector.Y;
		Z = vector.Z;
		W = scalarPart;
	}

	/// <summary>
	/// Creates a Quaternion from the given rotation matrix.
	/// </summary>
	/// <param name="matrix">The rotation matrix.</param>
	/// <returns>The created Quaternion.</returns>
	public static Quaternion CreateFromRotationMatrix(Matrix4 matrix)
	{
		double trace = matrix.M00 + matrix.M11 + matrix.M22;

		Quaternion q = new Quaternion();

		if (trace > 0.0f)
		{
			double s = (double)Math.Sqrt(trace + 1.0f);
			q.W = s * 0.5f;
			s = 0.5f / s;
			q.X = (matrix.M12 - matrix.M21) * s;
			q.Y = (matrix.M20 - matrix.M02) * s;
			q.Z = (matrix.M01 - matrix.M10) * s;
		}
		else
		{
			if (matrix.M00 >= matrix.M11 && matrix.M00 >= matrix.M22)
			{
				double s = (double)Math.Sqrt(1.0f + matrix.M00 - matrix.M11 - matrix.M22);
				double invS = 0.5f / s;
				q.X = 0.5f * s;
				q.Y = (matrix.M01 + matrix.M10) * invS;
				q.Z = (matrix.M02 + matrix.M20) * invS;
				q.W = (matrix.M12 - matrix.M21) * invS;
			}
			else if (matrix.M11 > matrix.M22)
			{
				double s = (double)Math.Sqrt(1.0f + matrix.M11 - matrix.M00 - matrix.M22);
				double invS = 0.5f / s;
				q.X = (matrix.M10 + matrix.M01) * invS;
				q.Y = 0.5f * s;
				q.Z = (matrix.M21 + matrix.M12) * invS;
				q.W = (matrix.M20 - matrix.M02) * invS;
			}
			else
			{
				double s = (double)Math.Sqrt(1.0f + matrix.M22 - matrix.M00 - matrix.M11);
				double invS = 0.5f / s;
				q.X = (matrix.M20 + matrix.M02) * invS;
				q.Y = (matrix.M21 + matrix.M12) * invS;
				q.Z = 0.5f * s;
				q.W = (matrix.M01 - matrix.M10) * invS;
			}
		}

		return q;
	}

	/// <summary>
	/// Creates a new Quaternion from the given yaw, pitch, and roll, in radians.
	/// </summary>
	/// <param name="xyz">X as pitch, Y as yaw and Z as roll</param>
	/// <returns></returns>
	public static Quaternion CreateFromYawPitchRoll(XYZ xyz)
	{
		return CreateFromYawPitchRoll(xyz.X, xyz.Y, xyz.Z);
	}

	/// <summary>
	/// Creates a new Quaternion from the given yaw, pitch, and roll, in radians.
	/// </summary>
	/// <param name="pitch">The pitch angle, around the X-axis.</param>
	/// <param name="yaw">The yaw angle, around the Y-axis.</param>
	/// <param name="roll">The roll angle, around the Z-axis.</param>
	/// <remarks>
	/// The values must be in radians
	/// </remarks>
	/// <returns></returns>
	public static Quaternion CreateFromYawPitchRoll(double pitch, double yaw, double roll)
	{
		double sr, cr, sp, cp, sy, cy;

		double halfPitch = pitch * 0.5f;
		sp = (double)Math.Sin(halfPitch);
		cp = (double)Math.Cos(halfPitch);

		double halfYaw = yaw * 0.5f;
		sy = (double)Math.Sin(halfYaw);
		cy = (double)Math.Cos(halfYaw);

		double halfRoll = roll * 0.5f;
		sr = (double)Math.Sin(halfRoll);
		cr = (double)Math.Cos(halfRoll);

		Quaternion result = new Quaternion
		{
			X = cy * sp * cr + sy * cp * sr,
			Y = sy * cp * cr - cy * sp * sr,
			Z = cy * cp * sr - sy * sp * cr,
			W = cy * cp * cr + sy * sp * sr
		};

		return result;
	}

	/// <summary>
	/// Creates a quaternion representing a rotation of a specified angle around a given axis.
	/// </summary>
	/// <remarks>The axis parameter should be a unit vector to ensure the resulting quaternion represents a valid
	/// rotation. The method assumes a right-handed coordinate system.</remarks>
	/// <param name="rotation">The rotation angle, in radians, to apply around the specified axis.</param>
	/// <param name="axis">The axis of rotation, represented as an <see cref="XYZ"/> vector. The vector should be normalized.</param>
	/// <returns>A <see cref="Quaternion"/> representing the rotation defined by the specified angle and axis.</returns>
	public static Quaternion FromAngleAxis(double rotation, XYZ axis)
	{
		double angle = (double)Math.Sin(rotation * 0.5);
		return new Quaternion(axis.X * angle, axis.Y * angle, axis.Z * angle, Math.Cos(rotation * 0.5));
	}

	/// <summary>
	/// Creates a quaternion representing the shortest rotation that aligns the specified origin vector with the destination
	/// vector.
	/// </summary>
	/// <remarks>If the origin and destination vectors are opposite, the method selects an arbitrary orthogonal axis
	/// for the rotation. Both input vectors should be normalized for accurate results.</remarks>
	/// <param name="orig">The initial direction vector to be rotated. Must be a non-zero vector.</param>
	/// <param name="dest">The target direction vector to align with. Must be a non-zero vector.</param>
	/// <returns>A quaternion that rotates the origin vector to align with the destination vector.</returns>
	public static Quaternion FromRotation(XYZ orig, XYZ dest)
	{
		double dot = orig.Dot(dest);

		XYZ axis;
		// Vectors are opposite, find an orthogonal axis
		if (dot <= -1.0 + MathHelper.Epsilon)
		{
			axis = XYZ.Cross(XYZ.AxisZ, orig);
			if (axis.GetLengthSquared() < MathHelper.Epsilon)
			{
				axis = XYZ.Cross(XYZ.AxisX, orig);
			}

			return FromAngleAxis(Math.PI, axis.Normalize());
		}

		axis = XYZ.Cross(orig, dest);
		double sinHalfAngleTimes2 = Math.Sqrt((1.0 + dot) * 2.0);
		double invSinHalfAngleTimes2 = 1.0 / sinHalfAngleTimes2;
		return new Quaternion(
			axis.X * invSinHalfAngleTimes2,
			axis.Y * invSinHalfAngleTimes2,
			axis.Z * invSinHalfAngleTimes2,
			sinHalfAngleTimes2 * 0.5);
	}

	/// <inheritdoc/>
	public bool Equals(Quaternion other)
	{
		return (X == other.X &&
				Y == other.Y &&
				Z == other.Z &&
				W == other.W);
	}

	/// <summary>
	/// Indicates whether the current object is equal to another object of the same type.
	/// </summary>
	/// <param name="other"></param>
	/// <param name="ndecimals">Number of decimals digits to be set as precision.</param>
	/// <returns></returns>
	public bool Equals(Quaternion other, int ndecimals)
	{
		return (Math.Round(X, ndecimals) == Math.Round(other.X, ndecimals) &&
				Math.Round(Y, ndecimals) == Math.Round(other.Y, ndecimals) &&
				Math.Round(Z, ndecimals) == Math.Round(other.Z, ndecimals) &&
				Math.Round(W, ndecimals) == Math.Round(other.W, ndecimals));
	}

	/// <summary>
	/// Get the Euler angles, in radians, for this quaternion.
	/// </summary>
	/// <returns></returns>
	public XYZ ToEulerAngles()
	{
		Quaternion q = this.Normalize();
		XYZ angles = new();

		// roll / x
		double sinr_cosp = 2 * (q.W * q.X + q.Y * q.Z);
		double cosr_cosp = 1 - 2 * (q.X * q.X + q.Y * q.Y);
		angles.X = (double)Math.Atan2(sinr_cosp, cosr_cosp);

		// pitch / y
		double sinp = 2 * (q.W * q.Y - q.Z * q.X);
		if (Math.Abs(sinp) >= 1)
		{
			angles.Y = Math.PI / 2 * Math.Sign(sinp);
		}
		else
		{
			angles.Y = (double)Math.Asin(sinp);
		}

		// yaw / z
		double siny_cosp = 2 * (q.W * q.Z + q.X * q.Y);
		double cosy_cosp = 1 - 2 * (q.Y * q.Y + q.Z * q.Z);
		angles.Z = (double)Math.Atan2(siny_cosp, cosy_cosp);

		return angles;
	}

	/// <summary>
	/// Create a rotation matrix
	/// </summary>
	/// <returns></returns>
	public Matrix4 ToMatrix()
	{
		return Matrix4.CreateFromQuaternion(this);
	}

	/// <inheritdoc/>
	public override string ToString()
	{
		return $"{X},{Y},{Z},{W}";
	}

	/// <inheritdoc/>
	public double this[int index]
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
				case 3:
					return W;
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
				case 3:
					W = value;
					break;
				default:
					throw new IndexOutOfRangeException($"The index must be between 0 and {this.Dimension}.");
			}
		}
	}
}