﻿using System;

namespace CSMath
{
	/// <summary>
	/// Contains the information for translate/scale/rotation or transform matrix to apply to a geometric shape.
	/// </summary>
	public class Transform
	{
		/// <summary>
		/// Rotation in Euler angles, the value is in radians.
		/// </summary>
		public XYZ EulerRotation
		{
			get { return this._rotation; }
			set
			{
				this._rotation = value;
				this.updateMatrix();
			}
		}

		/// <summary>
		/// Transform matrix.
		/// </summary>
		public Matrix4 Matrix { get { return this._matrix; } }

		/// <summary>
		/// Rotation represented in quaternion form.
		/// </summary>
		public Quaternion Quaternion
		{
			get
			{
				return Quaternion.CreateFromYawPitchRoll(this._rotation);
			}
		}

		/// <summary>
		/// Scale applied in the transformation.
		/// </summary>
		public XYZ Scale
		{
			get
			{
				return this._scale;
			}
			set
			{
				if (value.X == 0 || value.Y == 0 || value.Z == 0)
					throw new ArgumentException("Scale value cannot be 0");

				this._scale = value;
				this.updateMatrix();
			}
		}

		/// <summary>
		/// Rotation in Euler angles, the value is in radians.
		/// </summary>
		public XYZ Translation
		{
			get { return this._translation; }
			set
			{
				this._translation = value;
				this.updateMatrix();
			}
		}

		private Matrix4 _matrix;

		private XYZ _rotation = XYZ.Zero;

		private XYZ _scale = new XYZ(1, 1, 1);

		private XYZ _translation = XYZ.Zero;

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Transform()
		{
			this.Translation = XYZ.Zero;
			this.EulerRotation = XYZ.Zero;
			this.Scale = new XYZ(1, 1, 1);
		}

		/// <summary>
		/// Initialize a transform instance with the specified values.
		/// </summary>
		/// <param name="translation"></param>
		/// <param name="scale"></param>
		/// <param name="rotation">Rotation value in degrees.</param>
		public Transform(XYZ translation, XYZ scale, XYZ rotation) : this()
		{
			this.Translation = translation;
			this.Scale = scale;
			this.EulerRotation = rotation;
		}

		/// <summary>
		/// Initialize a transform instance with the specified matrix.
		/// </summary>
		/// <param name="matrix"></param>
		public Transform(Matrix4 matrix)
		{
			this._matrix = matrix;
			this.TryDecompose(out XYZ translation, out XYZ scaling, out Quaternion rotation);
			this._translation = translation;
			this._scale = scaling;
			this._rotation = rotation.ToEulerAngles();
		}

		public static Transform CreateTranslation(XYZ translation)
		{
			return new Transform(translation, new XYZ(1, 1, 1), XYZ.Zero);
		}

		public static Transform CreateRotation(XYZ angles)
		{
			return new Transform(Matrix4.CreateRotationMatrix(angles));
		}

		public static Transform CreateRotation(XYZ axis, double angle)
		{
			return new Transform(Matrix4.CreateFromAxisAngle(axis, angle));
		}

		public static Transform CreateScaling(XYZ scale)
		{
			return new Transform(Matrix4.CreateScale(scale));
		}

		public static Transform CreateScaling(XYZ scale, XYZ origin)
		{
			return new Transform(Matrix4.CreateScale(scale, origin));
		}

		/// <summary>
		/// Apply transform to a <see cref="XYZ"/>.
		/// </summary>
		/// <param name="xyz"></param>
		/// <param name="roundZero"></param>
		/// <returns></returns>
		public XYZ ApplyTransform(XYZ xyz, bool roundZero = true)
		{
			XYZ value = this._matrix * xyz;

			if (roundZero)
			{
				return value.RoundZero();
			}

			return value;
		}

		public XYZ Translate(XYZ xyz)
		{
			return xyz + this.Translation;
		}

		public XYZ Rotate(XYZ xyz)
		{
			var rotation = CreateRotation(this._rotation);
			return rotation.ApplyTransform(xyz);
		}

		/// <summary>
		/// Try to decompose the transform into it's components.
		/// </summary>
		/// <param name="translation"></param>
		/// <param name="scaling"></param>
		/// <param name="rotation"></param>
		/// <returns>true, if the decompose has succeeded.</returns>
		public bool TryDecompose(out XYZ translation, out XYZ scaling, out Quaternion rotation)
		{
			Matrix4 matrix = this._matrix;

			translation = new XYZ();
			scaling = new XYZ();
			rotation = new Quaternion();
			var XYZDouble = new XYZ();

			if (matrix.M33 == 0.0)
				return false;

			Matrix4 matrix4_3 = matrix;
			matrix4_3.M03 = 0.0;
			matrix4_3.M13 = 0.0;
			matrix4_3.M23 = 0.0;
			matrix4_3.M33 = 1.0;

			if (matrix4_3.GetDeterminant() == 0.0)
				return false;

			if (matrix.M03 != 0.0 || matrix.M13 != 0.0 || matrix.M23 != 0.0)
			{
				if (!Matrix4.Inverse(matrix, out Matrix4 inverse))
				{
					return false;
				}

				matrix.M03 = matrix.M13 = matrix.M23 = 0.0;
				matrix.M33 = 1.0;
			}

			translation.X = matrix.M30;
			matrix.M30 = 0.0;
			translation.Y = matrix.M31;
			matrix.M31 = 0.0;
			translation.Z = matrix.M32;
			matrix.M32 = 0.0;

			XYZ[] cols = new XYZ[3]
			{
			  new XYZ(matrix.M00, matrix.M01, matrix.M02),
			  new XYZ(matrix.M10, matrix.M11, matrix.M12),
			  new XYZ(matrix.M20, matrix.M21, matrix.M22)
			};

			scaling.X = cols[0].GetLength();
			cols[0] = cols[0].Normalize();
			XYZDouble.X = cols[0].Dot(cols[1]);
			cols[1] = cols[1] * 1 + cols[0] * -XYZDouble.X;

			scaling.Y = cols[1].GetLength();
			cols[1] = cols[1].Normalize();
			XYZDouble.Y = cols[0].Dot(cols[2]);
			cols[2] = cols[2] * 1 + cols[0] * -XYZDouble.Y;

			XYZDouble.Z = cols[1].Dot(cols[2]);
			cols[2] = cols[2] * 1 + cols[1] * -XYZDouble.Z;
			scaling.Z = cols[2].GetLength();
			cols[2] = cols[2].Normalize();

			XYZ rhs = XYZ.Cross(cols[1], cols[2]);
			if (cols[0].Dot(rhs) < 0.0)
			{
				for (int index = 0; index < 3; ++index)
				{
					scaling.X *= -1.0;
					cols[index].X *= -1.0;
					cols[index].Y *= -1.0;
					cols[index].Z *= -1.0;
				}
			}

			double trace = cols[0].X + cols[1].Y + cols[2].Z + 1.0;
			double qx;
			double qy;
			double qz;
			double qw;

			if (trace > 0)
			{
				double s = 0.5 / Math.Sqrt(trace);
				qx = (cols[2].Y - cols[1].Z) * s;
				qy = (cols[0].Z - cols[2].X) * s;
				qz = (cols[1].X - cols[0].Y) * s;
				qw = 0.25 / s;
			}
			else if (cols[0].X > cols[1].Y && cols[0].X > cols[2].Z)
			{
				double s = Math.Sqrt(1.0 + cols[0].X - cols[1].Y - cols[2].Z) * 2.0;
				qx = 0.25 * s;
				qy = (cols[0].Y + cols[1].X) / s;
				qz = (cols[0].Z + cols[2].X) / s;
				qw = (cols[2].Y - cols[1].Z) / s;
			}
			else if (cols[1].Y > cols[2].Z)
			{
				double s = Math.Sqrt(1.0 + cols[1].Y - cols[0].X - cols[2].Z) * 2.0;
				qx = (cols[0].Y + cols[1].X) / s;
				qy = 0.25 * s;
				qz = (cols[1].Z + cols[2].Y) / s;
				qw = (cols[0].Z - cols[2].X) / s;
			}
			else
			{
				double s = Math.Sqrt(1.0 + cols[2].Z - cols[0].X - cols[1].Y) * 2.0;
				qx = (cols[0].Z + cols[2].X) / s;
				qy = (cols[1].Z + cols[2].Y) / s;
				qz = 0.25 * s;
				qw = (cols[1].X - cols[0].Y) / s;
			}

			rotation.X = -qx;
			rotation.Y = -qy;
			rotation.Z = -qz;
			rotation.W = qw;

			return true;
		}

		private void updateMatrix()
		{
			Matrix4 translationMatrix = Matrix4.CreateTranslation(this._translation);
			Matrix4 rotationMatrix = Matrix4.CreateFromQuaternion(this.Quaternion);
			Matrix4 scaleMatrix = Matrix4.CreateScale(this._scale);

			this._matrix = translationMatrix * rotationMatrix * scaleMatrix;
		}
	}
}