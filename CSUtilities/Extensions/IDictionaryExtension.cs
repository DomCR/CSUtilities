using System.Collections.Generic;

namespace CSUtilities.Extensions;

/// <summary>
/// Extensions for <see cref="IDictionary{TKey, TValue}"/>
/// </summary>
public static class IDictionaryExtension
{
#if NETFRAMEWORK || NETSTANDARD2_0
	/// <summary>
	/// Removes the value with the specified key from the <see cref="IDictionary{TKey, TValue}"/>, and copies the element to the value parameter
	/// </summary>
	public static bool Remove<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, out TValue value)
	{
		if (dictionary.TryGetValue(key, out value))
		{
			dictionary.Remove(key);
			return true;
		}

		return false;
	}

	/// <summary>
	/// Attempts to add the specified key and value to the dictionary if the key does not already exist.
	/// </summary>
	/// <remarks>This method does not overwrite an existing value for the specified key. If the key already exists
	/// in the dictionary, the method returns false and does not modify the dictionary.</remarks>
	/// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
	/// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
	/// <param name="dictionary">The dictionary to which the key and value should be added. Cannot be null.</param>
	/// <param name="key">The key to add to the dictionary.</param>
	/// <param name="value">The value to associate with the specified key.</param>
	/// <returns>true if the key and value were added to the dictionary; otherwise, false.</returns>
	public static bool TryAdd<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value)
	{
		if (dictionary.ContainsKey(key))
		{
			return false;
		}

		dictionary.Add(key, value);
		return true;
	}
#endif
}
