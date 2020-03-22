using System;
using System.Collections.Generic;
using System.Text;

namespace CSUtilities.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool IsEmpty(this IEnumerable<object> enumerable)
        {
            return enumerable.GetEnumerator() == null;
        }
    }
}
