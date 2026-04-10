using System;

namespace CSMath;

/// <summary>
/// Provides helper methods and constants for common mathematical operations, including angle conversions,
/// floating-point comparisons, and rounding.
/// </summary>
/// <remarks>This static class includes constants for frequently used mathematical values and conversion factors
/// between degrees, radians, and gradians. It also offers utility methods for comparing floating-point numbers with a
/// configurable tolerance, normalizing angles, and rounding values. The methods are designed to address common issues
/// with floating-point precision and angle normalization in mathematical computations.</remarks>
public static class MathHelper
{
	/// <summary>
	/// Factor for converting an angle between degrees and gradians.
	/// </summary>
	public const double DegToGradFactor = 10.0 / 9.0;

	/// <summary>
	/// Factor for converting degrees to radians.
	/// </summary>
	public const double DegToRadFactor = (Math.PI / 180);

	/// <summary>
	/// Default tolerance
	/// </summary>
	public const double Epsilon = 1e-12;

	/// <summary>
	/// Factor for converting an angle between gradians and degrees.
	/// </summary>
	public const double GradToDegFactor = 9.0 / 10.0;

	/// <summary>
	/// Factor for converting an angle between gradians and radians.
	/// </summary>
	public const double GradToRadFactor = Math.PI / 200;

	/// <summary>
	/// Equivalent to 90 degrees.
	/// </summary>
	public const double HalfPI = Math.PI / 2;

	/// <summary>
	/// Equivalent to 180 degrees.
	/// </summary>
	public const double PI = Math.PI;

	/// <summary>
	/// Factor for converting radians to degrees.
	/// </summary>
	public const double RadToDegFactor = (180 / Math.PI);

	/// <summary>
	/// Factor for converting radians to gradians.
	/// </summary>
	public const double RadToGradFactor = 200 / Math.PI;

	/// <summary>
	/// Equivalent to 270 degrees.
	/// </summary>
	public const double ThreeHalfPI = 3 * Math.PI * 0.5;

	/// <summary>
	/// Equivalent to 360 degrees.
	/// </summary>
	public const double TwoPI = Math.PI * 2;

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

	/// <summary>
	/// Convert a value from degrees to gradian.
	/// </summary>
	/// <param name="value">Value in degree.</param>
	/// <returns>The gradian value.</returns>
	public static double DegToGrad(double value)
	{
		return value * DegToGradFactor;
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
	/// Returns zero if the specified number is within a small tolerance of zero; otherwise, returns the original number.
	/// </summary>
	/// <param name="number">The number to evaluate for near-zero equivalence.</param>
	/// <returns>Zero if the specified number is considered close enough to zero; otherwise, the original number.</returns>
	public static double FixZero(double number)
	{
		return FixZero(number, Epsilon);
	}

	/// <summary>
	/// Returns zero if the specified number is within the given threshold of zero; otherwise, returns the original number.
	/// </summary>
	/// <remarks>This method is useful for normalizing values that are very close to zero due to floating-point
	/// precision errors. It can help avoid issues where extremely small values are treated as nonzero in
	/// calculations.</remarks>
	/// <param name="number">The value to evaluate for near-zero equivalence.</param>
	/// <param name="threshold">The tolerance within which the number is considered to be zero. Must be non-negative.</param>
	/// <returns>Zero if the absolute value of the number is less than or equal to the threshold; otherwise, the original number.</returns>

	public static double FixZero(double number, double threshold)
	{
		return IsZero(number, threshold) ? 0 : number;
	}

	/// <summary>
	/// Returns a copy of the specified vector with components that are effectively zero replaced by exact zeros.
	/// </summary>
	/// <typeparam name="T">The type of vector. Must implement the IVector interface and have a parameterless constructor.</typeparam>
	/// <param name="vector">The vector to process. Components close to zero will be set to zero.</param>
	/// <returns>A new vector of type T with near-zero components set to zero.</returns>
	public static T FixZero<T>(T vector)
		where T : IVector, new()
	{
		return FixZero(vector, Epsilon);
	}

	/// <summary>
	/// Returns a new vector in which each component of the input vector is replaced with zero if its absolute value is
	/// less than the specified threshold.
	/// </summary>
	/// <remarks>This method does not modify the input vector. The returned vector is a new instance of type T. Use
	/// this method to eliminate near-zero noise in vector data.</remarks>
	/// <typeparam name="T">The type of vector. Must implement the IVector interface and have a parameterless constructor.</typeparam>
	/// <param name="vector">The input vector whose components are to be processed.</param>
	/// <param name="threshold">The threshold below which a component is considered to be zero. Components with an absolute value less than this
	/// value are set to zero.</param>
	/// <returns>A new vector of type T with components set to zero if their absolute value is less than the specified threshold;
	/// otherwise, the original component value is retained.</returns>
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
	/// Convert a value from gradian to degrees.
	/// </summary>
	/// <param name="value">Value in gradians.</param>
	/// <returns>The gradian value.</returns>
	public static double GradToDeg(double value)
	{
		return value * GradToDegFactor;
	}

	/// <summary>
	/// Convert a value from gradian to radian.
	/// </summary>
	/// <param name="value">Value in gradians</param>
	/// <returns>The gradian value.</returns>
	public static double GradToRad(double value)
	{
		return value * GradToRadFactor;
	}

	/// <summary>
	/// Determines whether the specified double-precision floating-point value is approximately zero, within a small
	/// tolerance.
	/// </summary>
	/// <param name="value">The value to compare to zero.</param>
	/// <returns>true if the value is within a small range of zero; otherwise, false.</returns>
	public static bool IsAlmostZero(double value)
	{
		if (value > -Epsilon)
		{
			return value < Epsilon;
		}
		return false;
	}

	/// <summary>
	/// Checks if a number is equal to another.
	/// </summary>
	/// <param name="a">Double precision number.</param>
	/// <param name="b">Double precision number.</param>
	/// <returns>True if its close to one or false in any other case.</returns>
	public static bool IsEqual(double a, double b)
	{
		return IsEqual(a, b, Epsilon);
	}

	/// <summary>
	/// Checks if a number is equal to another.
	/// </summary>
	/// <param name="a">Double precision number.</param>
	/// <param name="b">Double precision number.</param>
	/// <param name="threshold">Tolerance.</param>
	/// <returns>True if its close to one or false in any other case.</returns>
	public static bool IsEqual(double a, double b, double threshold)
	{
		return IsZero(Math.Abs(a) - Math.Abs(b), threshold);
	}

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

	/// <summary>
	/// Normalizes the value of an angle in degrees between 0-360.
	/// </summary>
	/// <param name="angle">Angle in degrees.</param>
	/// <returns>The equivalent angle in the range 0-360.</returns>
	/// <remarks>Negative angles will be converted to its positive equivalent.</remarks>
	public static double NormalizeAngle(double angle)
	{
		if (IsEqual(Math.Abs(angle), 360.0))
		{
			return 360;
		}

		double normalized = angle % 360.0;
		if (IsZero(normalized))
		{
			return 0.0;
		}

		if (normalized < 0)
		{
			return 360.0 + normalized;
		}

		return normalized;
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
		return NormalizeAngle(result);
	}

	/// <summary>
	/// Convert a value from radian to gradian.
	/// </summary>
	/// <param name="value">Value in radians.</param>
	/// <returns>The radian value.</returns>
	public static double RadToGrad(double value)
	{
		return value * RadToGradFactor;
	}

	/// <summary>
	/// Round off a numeric value to the nearest of another value.
	/// </summary>
	/// <param name="number">Number to round off.</param>
	/// <param name="roundTo">The number will be rounded to the nearest of this value.</param>
	/// <returns>The number rounded to the nearest value.</returns>
	public static double RoundToNearest(double number, double roundTo)
	{
		double multiplier = Math.Round(number / roundTo, 0);
		return multiplier * roundTo;
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
}