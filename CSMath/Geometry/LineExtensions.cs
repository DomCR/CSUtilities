using System;

namespace CSMath.Geometry;

public static class LineExtensions
{
	/// <summary>
	/// Creates a line from 2 points, the first point is the origin and the second point is used to calculate the direction.
	/// </summary>
	/// <typeparam name="T">The type of the line to create.</typeparam>
	/// <typeparam name="R">The type of the vector used for the points.</typeparam>
	/// <param name="pt1">The first point, which will be the origin of the line.</param>
	/// <param name="pt2">The second point, used to calculate the direction of the line.</param>
	/// <returns>A new instance of the specified line type.</returns>
	public static T CreateFromPoints<T, R>(R pt1, R pt2)
		where T : ILine<R>
		where R : IVector, new()
	{
		return (T)Activator.CreateInstance(typeof(T), pt1, pt2.Subtract(pt1));
	}

	/// <summary>
	/// Determines whether the specified point is on the line, or not.
	/// </summary>
	/// <param name="line">The line to check against.</param>
	/// <param name="point">The point to check.</param>
	/// <returns>True if the point is on the line; otherwise, false.</returns>
	public static bool IsPointOnLine<T>(this ILine<T> line, T point)
		where T : IVector
	{
		double lambda = 0;
		for (int i = 0; i < point.Dimension; ++i)
		{
			var value = (point[i] - line.Origin[i]) / line.Direction[i];
			if (i != 0 && (value != lambda))
			{
				return false;
			}

			lambda = value;
		}

		return true;
	}

	/// <summary>
	/// Tries to find the intersection between 2 lines.
	/// </summary>
	/// <typeparam name="T">The type of the first line.</typeparam>
	/// <typeparam name="S">The type of the second line.</typeparam>
	/// <typeparam name="R">The type of the vector used for the lines.</typeparam>
	/// <param name="line1">The first line.</param>
	/// <param name="line2">The second line.</param>
	/// <param name="intersection">The intersection point, if found.</param>
	/// <returns>True if the intersection is found.</returns>
	public static bool TryFindIntersection<T, S, R>(this T line1, S line2, out R intersection)
		where T : ILine<R>
		where S : ILine<R>
		where R : IVector
	{
		intersection = line1.FindIntersection(line2);

		return !intersection.IsNaN();
	}
}