using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSUtilities
{
   public static class EnumUtil
    {
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
        public static IEnumerable<string> GetValuesNames<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(o => o.ToString());
        }
    }
}
