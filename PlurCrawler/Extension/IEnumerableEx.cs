using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Extension
{
    public static class IEnumerableEx
    {
        public static void ForEach<T>(this IEnumerable<T> i, Action<T> action)
        {
            i.ToList().ForEach(action);
        }
    }
}
