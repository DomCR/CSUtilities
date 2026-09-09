namespace CSMath.Extensions;

public static class DoubleExtensions
{
	/// <summary>
	/// Determines whether the specified double is zero, using a default threshold for comparison.
	/// </summary>
	/// <param name="value">The double value to evaluate.</param>
	/// <returns>True if the number is zero within the default threshold; otherwise, false.</returns>
	public static bool IsZero(this double value)
	{
		return value.IsZero(MathHelper.Epsilon);
	}

	/// <summary>
	/// Determines whether the specified double is zero, using a custom threshold for comparison.
	/// </summary>
	/// <param name="value">The double value to evaluate.</param>
	/// <param name="threshold">The custom threshold for comparison.</param>
	/// <returns>True if the number is zero within the specified threshold; otherwise, false.</returns>
	public static bool IsZero(this double value, double threshold)
	{
		return value >= -threshold && value <= threshold;
	}
}