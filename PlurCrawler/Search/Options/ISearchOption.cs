using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Options
{
    public interface ISearchOption
    {
        string Query { get; set; }

        DateRange SearchRange { get; set; }

        ulong SearchCount { get; set; }
    }
}
