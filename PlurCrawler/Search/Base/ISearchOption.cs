using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Base
{
    public interface ISearchOption
    {
        string Query { get; set; }

        ulong SearchCount { get; set; }

        ulong Offset { get; set; }
    }
}
