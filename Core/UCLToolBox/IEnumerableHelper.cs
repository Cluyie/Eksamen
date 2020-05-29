using System;
using System.Collections.Generic;
using System.Linq;

namespace UCLToolBox
{
    public static class EnumerableHelper
    {
        /// <summary>
        /// Packs all elements in a collection to a singular string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static string ToSingularString<T>(this IEnumerable<T> collection)
        {
            return collection.Aggregate(string.Empty, (current, t) => current + (t.ToString() + "\n"));
        }
    }
}
