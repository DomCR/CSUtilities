namespace CSMath.Geometry
{
	public interface ILine<T>
		where T : IVector
	{
		public T Origin { get; set; }

		public T Direction { get; set; }
	}

	public struct Line2D
	{

	}

	public struct Line3D : ILine<XYZ>
	{
		public XYZ Origin { get; set; }

		public XYZ Direction { get; set; }
	}
}