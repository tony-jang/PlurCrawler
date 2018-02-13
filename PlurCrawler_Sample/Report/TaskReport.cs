using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Search;
using PlurCrawler.Search.Base;

namespace PlurCrawler_Sample.Report
{
    public class TaskReport
    {
        public DateTime SearchDate { get; set; }

        public int SearchCount { get; set; }

        public SearchResult SearchResult { get; set; }
        
        public ServiceKind RequestService { get; set; }

        public IEnumerable<ISearchResult> SearchData { get; set; }
    }
}
