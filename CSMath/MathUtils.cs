using System;

namespace CSMath
{
	public static class MathUtils
	{
		/// <summary>
		/// Factor for converting radians to degrees.
		/// </summary>
		public const double RadToDeg = (180 / Math.PI);

		/// <summary>
		/// Factor for converting degrees to radians.
		/// </summary>
		public const double DegToRad = (Math.PI / 180);

		/// <summary>
		/// Convert a value from degree to radian
		/// </summary>
		/// <param name="degree">Value in degrees</param>
		/// <returns>The radian value</returns>
		public static double ToRadian(double degree)
		{
			return degree * DegToRad;
		}

		/// <summary>
		/// Convert a vector from degree to radian
		/// </summary>
		/// <typeparam name="T">Vector in degrees</typeparam>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static T ToRadian<T>(T vector)
			where T : IVector<T>, new()
		{
			double[] components = vector.GetComponents();

			for (int i = 0; i < components.Length; i++)
			{
				components[i] = ToRadian(components[i]);
			}

			return new T().SetComponents(components);
		}

		/// <summary>
		/// Convert a value from radian to degree
		/// </summary>
		/// <param name="radian">Value in radians</param>
		/// <returns>The degree value</returns>
		public static double ToDegree(double radian)
		{
			return radian * RadToDeg;
		}

		/// <summary>
		/// Convert a vector from radian to degree
		/// </summary>
		/// <typeparam name="T">Vector in radians</typeparam>
		/// <param name="vector"></param>
		/// <returns></returns>
		public static T ToDegree<T>(T vector)
			where T : IVector<T>, new()
		{
			double[] components = vector.GetComponents();

			for (int i = 0; i < components.Length; i++)
			{
				components[i] = ToDegree(components[i]);
			}

			return new T().SetComponents(components);
		}
	}
}
