using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Search.Base;

namespace PlurCrawler.Search.Services.GoogleCSE
{
    /// <summary>
    /// 구글의 Custom Search 검색 결과입니다.
    /// </summary>
    public class GoogleCSESearchResult : ISearchResult
    {
        /// <summary>
        /// 해당 검색 결과가 올라온 날짜를 나타냅니다. null일 수 있습니다.
        /// </summary>
        public DateTime? PublishedDate { get; set; }

        /// <summary>
        /// 해당 검색 결과의 원본 URL을 나타냅니다.
        /// </summary>
        public string OriginalURL { get; set; }

        /// <summary>
        /// 해당 검색 결과의 제목을 나타냅니다.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 해당 검색 결과를 미리보기 합니다.
        /// </summary>
        public string Snippet { get; set; }

    }
}