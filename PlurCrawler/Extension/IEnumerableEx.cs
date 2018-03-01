using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Extension
{
    public static class IEnumerableEx
    {
        /// <summary>
        /// <see cref="IEnumerable{T}"/>의 각 요소에 대해 지정된 작업을 수행합니다.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="i"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> i, Action<T> action)
        {
            i.ToList().ForEach(action);
        }
    }
}
