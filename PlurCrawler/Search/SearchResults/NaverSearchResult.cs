using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.SearchResults
{
    public class NaverSearchResult : ISearchResult
    {
        public DateTime Date { get; set; }

        public string OriginalURL { get; set; }

        public string Title { get; set; }
    }
}
