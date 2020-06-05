using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CSUtilities.Extensions
{
	public static class ReflectionExtensions
	{
		public static PropertyInfo GetPropertyByName(this Type type, string name)
		{
			return type.GetProperties().FirstOrDefault(o => o.Name == name);
		}
	}
}
