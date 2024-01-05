using CSUtilities.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace CSUtilities.Extensions
{
	internal static class EnumExtensions
	{
		[Obsolete("Use Type.GetValues()")]
		public static IEnumerable<T> GetValues<T>()
		{
			return Enum.GetValues(typeof(T)).Cast<T>();
		}

		[Obsolete("Use Type.GetNames()")]
		public static IEnumerable<string> GetNames<T>(this T value)
			where T : Enum
		{
			return Enum.GetValues(typeof(T)).Cast<T>().Select(o => o.ToString());
		}

		public static T GetValueByName<T>(string name)
		{
			return Enum.GetValues(typeof(T)).Cast<T>().FirstOrDefault(o => o.ToString() == name);
		}

		public static T Parse<T>(string value, bool ignoreCase = false)
			where T : Enum
		{
			return (T)Enum.Parse(typeof(T), value, ignoreCase);
		}

		public static bool TryParse<T>(string value, out T result, bool ignoreCase = false)
			where T : Enum
		{
			try
			{
				result = Parse<T>(value, ignoreCase);
				return true;
			}
			catch (Exception)
			{
				result = default(T);
				return false;
			}
		}

		/// <summary>
		/// Gets a string value for a particular enum value.
		/// </summary>
		/// <param name="value">enum value</param>
		/// <returns>String Value associated via a <see cref="StringValueAttribute"/> attribute, or null if not found.</returns>
		public static string GetStringValue<T>(this T value)
			where T : Enum
		{
			Type type = value.GetType();

			FieldInfo fi = type.GetField(value.ToString());
			return fi.GetCustomAttribute<StringValueAttribute>()?.Value;
		}
	}
}