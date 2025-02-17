namespace CSMath
{
	public partial struct Matrix3
	{
		public static XYZ operator *(Matrix3 matrix, XYZ value)
		{
			return new XYZ(matrix.m00 * value.X + matrix.m01 * value.Y + matrix.m02 * value.Z,
							matrix.m10 * value.X + matrix.m11 * value.Y + matrix.m12 * value.Z,
							matrix.m20 * value.X + matrix.m21 * value.Y + matrix.m22 * value.Z);
		}
	}
}
