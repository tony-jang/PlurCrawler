using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Options
{
    class NaverSearchOption : ISearchOption
    {
        public string Query { get; set; }

        public DateRange SearchRange { get; set; }

        public ulong SearchCount { get; set; }
    }
}
