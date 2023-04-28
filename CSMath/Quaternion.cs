using System;

namespace CSMath
{
	/// <summary>
	/// Four dimensional vector which is used to efficiently rotate an object about the (x,y,z) vector by the angle theta, where w = cos(theta/2).
	/// </summary>
	public struct Quaternion
	{
		/// <summary>
		/// Specifies the X-value of the vector component of the Quaternion.
		/// </summary>
		public double X;

		/// <summary>
		/// Specifies the Y-value of the vector component of the Quaternion.
		/// </summary>
		public double Y;

		/// <summary>
		/// Specifies the Z-value of the vector component of the Quaternion.
		/// </summary>
		public double Z;

		/// <summary>
		/// Specifies the rotation component of the Quaternion.
		/// </summary>
		public double W;

		/// <summary>
		/// Returns a Quaternion representing no rotation. 
		/// </summary>
		public static Quaternion Identity
		{
			get { return new Quaternion(0, 0, 0, 1); }
		}

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
		/// <param name="vectorPart">The vector part of the Quaternion.</param>
		/// <param name="scalarPart">The rotation part of the Quaternion.</param>
		public Quaternion(XYZ vectorPart, double scalarPart)
		{
			X = vectorPart.X;
			Y = vectorPart.Y;
			Z = vectorPart.Z;
			W = scalarPart;
		}

		/// <summary>
		/// Creates a new Quaternion from the given yaw, pitch, and roll, in radians.
		/// </summary>
		/// <param name="xyz">The yaw angle, in radians, around the Y-axis.</param>
		/// <param name="yaw">The yaw angle, in radians, around the Y-axis.</param>
		/// <param name="pitch">The pitch angle, in radians, around the X-axis.</param>
		/// <param name="roll">The roll angle, in radians, around the Z-axis.</param>
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

		/// <inheritdoc/>
		public override string ToString()
		{
			return $"{X},{Y},{Z},{W}";
		}
	}
}
