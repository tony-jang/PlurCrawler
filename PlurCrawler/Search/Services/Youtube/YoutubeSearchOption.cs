using PlurCrawler.Search.Base;
using PlurCrawler.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Services.Youtube
{
    public class YoutubeSearchOption : IDateSearchOption
    {
        /// <summary>
        /// 검색할 검색어입니다.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// 검색할 갯수입니다.
        /// </summary>
        public ulong SearchCount { get; set; }

        /// <summary>
        /// 페이지의 오프셋을 결정합니다. 예를 들어 4를 입력했다면 5번째 결과부터 출력됩니다.
        /// </summary>
        public ulong Offset { get; set; }

        /// <summary>
        /// 검색할 언어 코드를 입력합니다.
        /// </summary>
        public LanguageCode Language { get; set; }

        /// <summary>
        /// 검색 결과가 나온 결과의 범위를 지정합니다.
        /// </summary>
        public DateRange PublishedDateRange { get; set; }
    }
}
