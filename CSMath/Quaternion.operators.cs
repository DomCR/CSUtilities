namespace CSMath;

public partial struct Quaternion
{
	public static XYZ operator *(Quaternion q, XYZ v)
	{
		XYZ u = new XYZ((float)q.X, (float)q.Y, (float)q.Z);
		XYZ cr1 = XYZ.Cross(u, v);
		XYZ cr2 = XYZ.Cross(u, cr1);
		return v + (cr1 * (float)q.W + cr2) * 2f;
	}
}
