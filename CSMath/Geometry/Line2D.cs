namespace CSMath.Geometry
{
	public struct Line2D : ILine<XY>
	{
		/// <inheritdoc/>
		public XY Origin { get; set; }

		/// <inheritdoc/>
		public XY Direction { get; set; }
	}
}