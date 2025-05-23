﻿using System;
using System.ComponentModel;

namespace CSUtilities
{
#if PUBLIC
	public
#else
	internal
#endif
		static class EnvironmentVars
	{
		public static void Set(string name, string value, EnvironmentVariableTarget target)
		{
			Environment.SetEnvironmentVariable(name, value, target);
		}

		public static void Set(string name, string value)
		{
			Environment.SetEnvironmentVariable(name, value, EnvironmentVariableTarget.Process);
		}

		public static void SetIfNull(string name, string value)
		{
			if (Get(name) == null)
			{
				Set(name, value);
			}
		}

		public static void SetIfNull(string name, string value, EnvironmentVariableTarget target)
		{
			if (Get(name) == null)
			{
				Set(name, value);
			}
		}

		public static string Get(string name)
		{
			return Environment.GetEnvironmentVariable(name, EnvironmentVariableTarget.Process);
		}

		public static string Get(string name, EnvironmentVariableTarget target)
		{
			return Environment.GetEnvironmentVariable(name, target);
		}

		public static T Get<T>(string name)
		{
			return Get<T>(name, EnvironmentVariableTarget.Process);
		}

		public static T Get<T>(string name, EnvironmentVariableTarget target)
		{
			string value = Environment.GetEnvironmentVariable(name, target);
			if (value == null)
			{
				return default;
			}

			return (T)TypeDescriptor.GetConverter(typeof(T)).ConvertFromInvariantString(value);
		}

		public static void Delete(string name)
		{
			Environment.SetEnvironmentVariable(name, null, EnvironmentVariableTarget.Process);
		}

		public static void Delete(string name, EnvironmentVariableTarget target)
		{
			Environment.SetEnvironmentVariable(name, null, target);
		}
	}
}
