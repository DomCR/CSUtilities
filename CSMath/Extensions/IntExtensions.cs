namespace CSMath.Extensions;

public static class IntExtensions
{
	/// <summary>
	/// Determines whether the specified integer is even.
	/// </summary>
	/// <param name="value">The integer to evaluate.</param>
	/// <returns>True if the integer is even; otherwise, false.</returns>
	public static bool IsEven(this int value)
	{
		return (value & 1) == 0;
	}

	/// <summary>
	/// Determines whether the specified integer is odd.
	/// </summary>
	/// <param name="value">The integer to evaluate.</param>
	/// <returns>True if the integer is odd; otherwise, false.</returns>
	public static bool IsNegative(this int value)
	{
		return value < 0;
	}

	/// <summary>
	/// Determines whether the specified integer is odd.
	/// </summary>
	/// <param name="value">The integer to evaluate.</param>
	/// <returns>True if the integer is odd; otherwise, false.</returns>
	public static bool IsOdd(this int value)
	{
		return (value & 1) == 1;
	}
}