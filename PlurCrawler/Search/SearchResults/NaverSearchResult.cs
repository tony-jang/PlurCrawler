using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.SearchResults
{
    /// <summary>
    /// 네이버 검색 결과에 대해서 나타냅니다.
    /// </summary>
    public class NaverSearchResult : ISearchResult
    {
        public DateTime Date { get; set; }

        public string OriginalURL { get; set; }

        public string Title { get; set; }
    }
}
