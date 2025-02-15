using System;
using System.Collections.Generic;
using System.Text;

namespace CSUtilities.Exceptions
{
#if PUBLIC
	public
#else
	internal
#endif
	static class ValueExtensions
	{
		public delegate bool Check<T>(T obj);

		public static void ThrowIfNull(this object parameter)
		{
			if (parameter != null)
				return;

			throw new System.ArgumentNullException();
		}

		public static void ThrowIfNull(this object parameter, string paramName)
		{
			if (parameter != null)
				return;

			throw new System.ArgumentNullException(paramName);
		}

		public static void ThrowIfNull(this object parameter, string paramName, string message)
		{
			if (parameter != null)
				return;

			throw new System.ArgumentNullException(paramName, message);
		}

		public static void ThrowIf<T, E>(this T parameter, Check<T> check)
			where E : Exception, new()
		{
			if (check(parameter))
			{
				throw new E();
			}
		}

		public static void ThrowIf<T, E>(this T parameter, Check<T> check, string message)
			where E : Exception, new()
		{
			if (check(parameter))
			{
				throw Activator.CreateInstance(typeof(E), message) as E;
			}
		}

		public static void InRange<T>(this T parameter, T min, T max, string? message = null, bool inclusive = true)
			where T : IComparable<T>
		{
			int down = parameter.CompareTo(min);
			int up = parameter.CompareTo(max);

			if (up > 0 && down < 0)
			{
				throw new ArgumentOutOfRangeException(nameof(parameter), message);
			}
		}
	}
}
