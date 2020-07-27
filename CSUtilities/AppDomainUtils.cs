using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSUtilities
{
	internal static class AppDomainUtils
	{
		public static Type[] GetTypesOfInterface<T>()
		{
			return AppDomain.CurrentDomain.GetAssemblies()
				.SelectMany(s => s.GetTypes())
				.Where(p => p.GetInterface(typeof(T).FullName) != null)
				.ToArray();
		}

		public static Type[] GetTypesWithAttribute<T>() where T : Attribute
		{
			return AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
			.Where(p => p.GetCustomAttribute<T>() != null).ToArray();
		}
	}
}
