using System;
using System.Runtime.CompilerServices;

namespace CSUtilities.Extensions
{
#if PUBLIC
	public
#else
	internal
#endif
	static class ObjectExtensions
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

		public static void InRange<T>(this T parameter, T min, T max, string message, bool inclusive = true, [CallerMemberName] string name = null)
			where T : struct, IComparable<T>
		{
			int up = parameter.CompareTo(max);

			if (up < 0)
			{
				throw new ArgumentOutOfRangeException(name, parameter, message);
			}
		}
	}
}
