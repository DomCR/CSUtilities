﻿using System;

namespace CSMath
{
	public static class MathHelper
	{
		/// <summary>
		/// Factor for converting radians to degrees.
		/// </summary>
		public const double RadToDegFactor = (180 / Math.PI);

		/// <summary>
		/// Factor for converting degrees to radians.
		/// </summary>
		public const double DegToRadFactor = (Math.PI / 180);

		/// <summary>
		/// Default tolerance
		/// </summary>
		public const double Epsilon = 1e-12;

		public const double TwoPI = Math.PI * 2;

		/// <summary>
		/// Checks if a number is close to zero.
		/// </summary>
		/// <param name="number">Double precision number.</param>
		/// <returns>True if its close to one or false in any other case.</returns>
		public static bool IsZero(double number)
		{
			return IsZero(number, Epsilon);
		}

		/// <summary>
		/// Checks if a number is close to zero.
		/// </summary>
		/// <param name="number">Double precision number.</param>
		/// <param name="threshold">Tolerance.</param>
		/// <returns>True if its close to one or false in any other case.</returns>
		public static bool IsZero(double number, double threshold)
		{
			return number >= -threshold && number <= threshold;
		}

		public static double FixZero(double number)
		{
			return FixZero(number, Epsilon);
		}

		public static double FixZero(double number, double threshold)
		{
			return IsZero(number, threshold) ? 0 : number;
		}

		public static T FixZero<T>(T vector)
			where T : IVector, new()
		{
			return FixZero(vector, Epsilon);
		}

		public static T FixZero<T>(T vector, double threshold)
			where T : IVector, new()
		{
			T result = new T();

			for (int i = 0; i < vector.Dimension; i++)
			{
				result[i] = FixZero(vector[i], threshold);
			}

			return result;
		}

		/// <summary>
		/// Convert a value from radian to degree
		/// </summary>
		/// <param name="value">Value in radians</param>
		/// <param name="absolute">Calculates the negative values in a 0-360 range.</param>
		/// <returns>The radian value</returns>
		public static double RadToDeg(double value, bool absolute = true)
		{
			var result = value * RadToDegFactor;

			result %= 360;

			if (result < 0)
			{
				result = 360 + result;
			}

			return result;
		}

		/// <summary>
		/// Convert a value from degree to radian
		/// </summary>
		/// <param name="value">Value in degrees</param>
		/// <returns>The radian value</returns>
		public static double DegToRad(double value)
		{
			return value * DegToRadFactor;
		}

		/// <summary>
		/// Returns the sine of specific angle in radians adjusting the value to 0 using <see cref="Epsilon"/> as tolerance.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static double Sin(double value)
		{
			double result = Math.Sin(value);
			return IsZero(result) ? 0 : result;
		}

		/// <summary>
		/// Returns the cosine of specific angle in radians adjusting the value to 0 using <see cref="Epsilon"/> as tolerance.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public static double Cos(double value)
		{
			double result = Math.Cos(value);
			return IsZero(result) ? 0 : result;
		}
	}
}
