using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSUtilities.Extensions
{
    public static class QueueExtensions
    {
        public static T SafeDequeue<T>(this Queue<T> q)
        {
            if (!q.Any())
            {
                return default;
            }
            else
            {
                return q.Dequeue();
            }
        }
        public static bool SafeDequeue<T>(this Queue<T> q, out T element)
        {
            if (!q.Any())
            {
                element = default;
                return false;
            }
            else
            {
                element = q.Dequeue();
                return true;
            }
        }
    }
}
