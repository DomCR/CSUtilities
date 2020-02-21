using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSUtilities
{
   public static class EnumUtils
    {
        [Obsolete("Use Type.GetValues()")]
        public static IEnumerable<T> GetValues<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>();
        }
        [Obsolete("Use Type.GetNames()")]
        public static IEnumerable<string> GetValuesNames<T>()
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(o => o.ToString());
        }
    }
}
