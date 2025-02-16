namespace CSMath
{
	public partial struct Matrix3
	{
		/// <summary>
		/// 4-dimensional zero matrix.
		/// </summary>
		public static readonly Matrix3 Zero = new Matrix3(
			0.0, 0.0, 0.0,
			0.0, 0.0, 0.0,
			0.0, 0.0, 0.0);

		/// <summary>
		/// 4-dimensional identity matrix.
		/// </summary>
		public static readonly Matrix3 Identity = new Matrix3(
			1.0, 0.0, 0.0,
			0.0, 1.0, 0.0,
			0.0, 0.0, 1.0);

		#region Public Fields

		/// <summary>
		/// Value at column 0, row 0 of the matrix.
		/// </summary>
		public double m00;
		/// <summary>
		/// Value at column 0, row 1 of the matrix.
		/// </summary>
		public double m01;
		/// <summary>
		/// Value at column 0, row 2 of the matrix.
		/// </summary>
		public double m02;

		/// <summary>
		/// Value at column 1, row 0 of the matrix.
		/// </summary>
		public double m10;
		/// <summary>
		/// Value at column 1, row 1 of the matrix.
		/// </summary>
		public double m11;
		/// <summary>
		/// Value at column 1, row 2 of the matrix.
		/// </summary>
		public double m12;

		/// <summary>
		/// Value at column 2, row 0 of the matrix.
		/// </summary>
		public double m20;
		/// <summary>
		/// Value at column 2, row 1 of the matrix.
		/// </summary>
		public double m21;
		/// <summary>
		/// Value at column 2, row 2 of the matrix.
		/// </summary>
		public double m22;

		#endregion Public Fields

		public Matrix3(
			double m00, double m10, double m20,
			double m01, double m11, double m21,
			double m02, double m12, double m22)
		{
			//Col 0
			this.m00 = m00;
			this.m01 = m01;
			this.m02 = m02;

			//Col 1
			this.m10 = m10;
			this.m11 = m11;
			this.m12 = m12;

			//Col 2
			this.m20 = m20;
			this.m21 = m21;
			this.m22 = m22;
		}
	}
}
