namespace CSMath.Geometry
{
	public struct Line3D : ILine<XYZ>
	{
		/// <inheritdoc/>
		public XYZ Origin { get; set; }

		/// <inheritdoc/>
		public XYZ Direction { get; set; }
	}
}