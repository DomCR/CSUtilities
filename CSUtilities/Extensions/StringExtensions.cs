using System;
using System.Collections.Generic;
using System.Text;

namespace CSUtilities.Extensions
{
    static class StringExtensions
    {
        /// <summary>
        /// Gets a string and returns an array of bytes.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static byte[] ToByteArray(this string value)
        {
            return Enumerable.Range(0, value.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(value.Substring(x, 2), 16))
                .ToArray();
        }
    }
}
