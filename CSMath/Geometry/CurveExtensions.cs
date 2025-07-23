using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace CSMath.Geometry
{
	public static class CurveExtensions
	{
		public static XYZ PolarCoordinateRelativeToCenter(double angle, XYZ center, XYZ normal, XYZ startPoint, double ratio = 1)
		{
			XYZ prep = XYZ.Cross(normal, startPoint);
			double radius = startPoint.Normalize().GetLength();
			return radius * (Math.Cos(angle) * startPoint + (double)ratio * Math.Sin(angle) * prep) + center;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="precision"></param>
		/// <param name="center"></param>
		/// <param name="startAngle"></param>
		/// <param name="endAngle"></param>
		/// <param name="normal"></param>
		/// <param name="startPoint">Start point in the curve.</param>
		/// <param name="ratio"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException"></exception>
		public static List<XYZ> PolygonalVertexes(int precision, XYZ center, double startAngle, double endAngle, XYZ normal, XYZ startPoint, double ratio = 1)
		{
			if (precision < 2)
			{
				throw new ArgumentOutOfRangeException(nameof(precision), precision, "The precision must be equal or greater than two.");
			}

			List<XYZ> points = new();

			double end = endAngle;
			if (end <= startAngle)
			{
				end += MathHelper.TwoPI;
			}

			XYZ xdir = (startPoint - center).Normalize();
			XYZ ydir = XYZ.Cross(normal, xdir);

			double delta = (end - startAngle) / (precision - 1);
			double radius = center.DistanceFrom(startPoint);

			double start = 0;
			for (int i = 0; i < precision; i++, start += delta)
			{
				points.Add(MathHelper.Cos(start) * xdir * radius + (double)ratio * MathHelper.Sin(start) * ydir * radius + center);
			}

			return points;
		}
	}
}