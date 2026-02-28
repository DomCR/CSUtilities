using System;
using System.Collections.Generic;
using System.Linq;

namespace CSUtilities.Extensions;

/// <summary>
/// Estensions for <see cref="IEnumerable{T}"/>
/// </summary>
internal static class IEnumerableExtensions
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

	/// <summary>
	/// Gets the element in an specific index or it's default value
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="enumerable"></param>
	/// <param name="index"></param>
	/// <param name="result"></param>
	/// <returns></returns>
	public static bool TryGet<T>(this IEnumerable<T> enumerable, int index, out T result)
	{
		if (enumerable.Count() < index)
		{
			result = default(T);
			return false;
		}

		result = enumerable.ElementAt(index);
		return true;
	}

		/// <summary>
		/// Returns a sequence containing all elements of the source collection except for consecutive elements equal to the
		/// specified value at the end.
		/// </summary>
		/// <remarks>Equality is determined using the <see cref="object.Equals(object)"/> method. The order of
		/// elements in the returned sequence matches the source collection. The operation is performed eagerly; the entire
		/// collection is enumerated and copied before processing.</remarks>
		/// <typeparam name="T">The type of elements in the source collection.</typeparam>
		/// <param name="enumerable">The source collection from which to remove trailing elements equal to <paramref name="element"/>. Cannot be null.</param>
		/// <param name="element">The value to compare against elements at the end of the collection. All consecutive elements equal to this value
		/// at the end will be removed.</param>
		/// <returns>An <see cref="IEnumerable{T}"/> containing the elements of the source collection with trailing elements equal to
		/// <paramref name="element"/> removed. If no trailing elements match, the original sequence is returned.</returns>
		public static IEnumerable<T> RemoveLastEquals<T>(this IEnumerable<T> enumerable, T element)
		{
			List<T> lst = new List<T>(enumerable);
			while (lst.Last().Equals(element))
			{
				lst.RemoveAt(lst.Count - 1);
			}

			return lst;
		}

		/// <summary>
		/// Performs the specified action on each element of the source sequence.
		/// </summary>
		/// <remarks>This method enumerates the entire sequence and invokes the provided action for each element in
		/// order. If the sequence is empty, the action is not invoked. The method does not modify the source
		/// sequence.</remarks>
		/// <typeparam name="T">The type of the elements in the source sequence.</typeparam>
		/// <param name="enumerable">The sequence of elements to iterate over. Cannot be null.</param>
		/// <param name="action">The action to perform on each element of the sequence. Cannot be null.</param>
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
		{
			foreach (T item in enumerable)
			{
				action(item);
			}
		}
	}
}
