using System;
using System.Collections.Generic;
using System.Text;

namespace CSUtilities.Extensions
{
	/// <summary>
	/// Estensions for <see cref="IEnumerable{T}"/>.
	/// </summary>
	public static class IEnumerableExtensions
	{
		/// <summary>
		/// Return true if the collection is empty.
		/// </summary>
		/// <param name="enumerable"></param>
		/// <returns></returns>
		public static bool IsEmpty<T>(this IEnumerable<T> enumerable)
		{
			return enumerable.GetEnumerator() == null;
		}
		/// <summary>
		/// Transforms an enumerable into a Queue.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumerable"></param>
		/// <returns></returns>
		public static Queue<T> ToQueue<T>(this IEnumerable<T> enumerable)
		{
			return new Queue<T>(enumerable);
		}
	}
}
