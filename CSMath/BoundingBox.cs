namespace CSMath
{
	/// <summary>
	/// Bounding box representation aligned to XYZ axis
	/// </summary>
	public struct BoundingBox
	{
		/// <summary>
		/// Get the min corner of the bounding box
		/// </summary>
		public XYZ Min { get; set; }

		/// <summary>
		/// Get the max corner of the bounding box
		/// </summary>
		public XYZ Max { get; set; }

		/// <summary>
		/// Center of the box
		/// </summary>
		public XYZ Center
		{
			get
			{
				return this.Min + (this.Max - this.Min) * 0.5;
			}
		}

		/// <summary>
		/// Bounding box constructor with 2 points
		/// </summary>
		/// <param name="min"></param>
		/// <param name="max"></param>
		public BoundingBox(XYZ min, XYZ max)
		{
			this.Min = new XYZ(System.Math.Min(min.X, max.X), System.Math.Min(min.Y, max.Y), System.Math.Min(min.Z, max.Z));
			this.Max = new XYZ(System.Math.Max(min.X, max.X), System.Math.Max(min.Y, max.Y), System.Math.Max(min.Z, max.Z));
		}

		/// <summary>
		/// Bounding box contructor
		/// </summary>
		/// <param name="minX"></param>
		/// <param name="minY"></param>
		/// <param name="minZ"></param>
		/// <param name="maxX"></param>
		/// <param name="maxY"></param>
		/// <param name="maxZ"></param>
		public BoundingBox(double minX, double minY, double minZ, double maxX, double maxY, double maxZ)
		{
			this = new BoundingBox(new XYZ(minX, minY, minZ), new XYZ(maxX, maxY, maxZ));
		}
	}
}
