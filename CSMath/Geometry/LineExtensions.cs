using System;

namespace CSMath.Geometry
{
	public static class LineExtensions
	{
		public static T CreateFromPoints<T, R>(R pt1, R pt2)
			where T : ILine<R>
			where R : IVector
		{

			throw new NotImplementedException();
		}

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