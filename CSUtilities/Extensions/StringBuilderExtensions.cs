using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSUtilities.Extensions
{
	/// <summary>
	/// StringBuilder utility extensions.
	/// </summary>
#if PUBLIC
	public
#else
	internal
#endif
	static class StringBuilderExtensions
	{
#if NETFRAMEWORK
		/// <summary>
		/// 
		/// </summary>
		public static StringBuilder AppendJoin<T>(this StringBuilder sb, string? separator, IEnumerable<T> values)
		{
			separator ??= string.Empty;

			for (var i = 0; i < values.Count(); i++)
			{
				if (i > 0)
				{
					sb.Append(separator);
				}

				sb.Append(values.ElementAt(i));
			}

			return sb;
		}
#endif
	}
}
