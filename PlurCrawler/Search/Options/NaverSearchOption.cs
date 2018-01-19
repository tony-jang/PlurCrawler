using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Search.Options.Base;
using PlurCrawler.Search.Options.Tools;

namespace PlurCrawler.Search.Options
{
    /// <summary>
    /// 네이버 검색 시의 옵션을 나타내는 클래스입니다.
    /// </summary>
    public class NaverSearchOption : ISearchOption
    {
        /// <summary>
        /// 검색어 입니다.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// 총 검색할 갯수를 의미합니다.
        /// </summary>
        public ulong SearchCount { get; set; } = 10;

        public SearchSortOptions NaverSortOptions { get; set; }
    }
}
