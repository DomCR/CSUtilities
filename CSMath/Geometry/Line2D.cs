namespace CSMath.Geometry
{
	public interface ILine<T>
		where T : IVector
	{
		/// <summary>
		/// Origin point that the line intersects with
		/// </summary>
		public T Origin { get; set; }

		/// <summary>
		/// Direction fo the line
		/// </summary>
		public T Direction { get; set; }
	}

	public struct Line2D : ILine<XY>
	{
		/// <inheritdoc/>
		public XY Origin { get; set; }

		/// <inheritdoc/>
		public XY Direction { get; set; }
	}

	public struct Line3D : ILine<XYZ>
	{
		/// <inheritdoc/>
		public XYZ Origin { get; set; }

		/// <inheritdoc/>
		public XYZ Direction { get; set; }
	}

	public static class LineExtensions
	{
		/// <summary>
		/// Determines whether the specified point is on the line, or not.
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		public static bool IsPointOnLine<T>(this ILine<T> line, T point)
			where T : IVector
		{
			double lambda = 0;
			double[] components = point.GetComponents();

			for (int i = 0; i < components.Length; ++i)
			{
				var value = (components[i] - line.Origin.GetComponents()[i]) / line.Direction.GetComponents()[i];
				if (i != 0 && (value != lambda))
				{
					return false;
				}

				lambda = value;
			}

			return true;
		}
	}
}