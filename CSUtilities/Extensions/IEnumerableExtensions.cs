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
        public static bool IsEmpty(this IEnumerable<object> enumerable)
        {
            return enumerable.GetEnumerator() == null;
        }
    }
}
