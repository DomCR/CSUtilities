using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CSUtilities;

#if PUBLIC
public
#else
internal
#endif
	static class AppDomainUtils
{
	public static IEnumerable<Type> GetTypesOfInterface<T>()
	{
		return AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(getLoadableTypes)
			.Where(p => p.GetInterface(typeof(T).FullName) != null);
	}

	public static IEnumerable<Type> GetTypesWithAttribute<T>() where T : Attribute
	{
		return AppDomain.CurrentDomain.GetAssemblies()
			.SelectMany(getLoadableTypes)
			.Where(p => p.GetCustomAttribute<T>() != null);
	}

	private static IEnumerable<Type> getLoadableTypes(Assembly assembly)
	{
		try
		{
			return assembly.GetTypes();
		}
		catch (ReflectionTypeLoadException ex)
		{
			return ex.Types.Where(t => t != null);
		}
	}
}