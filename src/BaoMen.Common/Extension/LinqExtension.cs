using System;
using System.Collections.Generic;
using System.Text;

namespace BaoMen.Common.Extension
{
    /// <summary>
    /// Linq查询的扩展方法
    /// </summary>
    public static class LinqExtension
    {
        /// <summary>
        /// 用于Linq的去重,扩展方法需要放到静态类中
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source">源</param>
        /// <param name="keySelector">键选择器</param>
        /// <returns></returns>
        public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            HashSet<TKey> seenKeys = new HashSet<TKey>();
            foreach (TSource element in source)
            {
                if (seenKeys.Add(keySelector(element)))
                {
                    yield return element;
                }
            }
        }
    }
}
