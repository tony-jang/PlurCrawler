using PlurCrawler.Search.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Services.Youtube
{
    public class YoutubeSearchResult : ISearchResult
    {
        /// <summary>
        /// 해당 유튜브 영상이 게시된 날짜를 나타냅니다.
        /// </summary>
        public DateTime? PublishedDate { get; set; }

        /// <summary>
        /// 오리지널 URL을 가져옵니다.
        /// </summary>
        public string OriginalURL { get; set; }

        public string Title { get; set; }

        public string ChannelTitle { get; set; }

        public string Description { get; set; }

        public string ChannelId { get; set; }

    }
}
