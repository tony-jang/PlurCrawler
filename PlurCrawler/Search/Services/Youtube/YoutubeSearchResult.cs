using System;

using PlurCrawler.Attributes;
using PlurCrawler.Search.Base;

namespace PlurCrawler.Search.Services.Youtube
{
    public class YoutubeSearchResult : ISearchResult
    {
        /// <summary>
        /// 해당 유튜브 영상이 게시된 날짜를 나타냅니다.
        /// </summary>
        [MySQLType("DATETIME")]
        public DateTime? PublishedDate { get; set; }

        /// <summary>
        /// 해당 동영상의 원본 URL을 가져옵니다.
        /// </summary>
        [MySQLType("TEXT")]
        public string OriginalURL { get; set; }

        /// <summary>
        /// 해당 동영상의 제목을 가져옵니다.
        /// </summary>
        [PrimaryKey]
        [MySQLType("VARCHAR(300)")]
        public string Title { get; set; }

        /// <summary>
        /// 해당 동영상 게시자의 제목을 가져옵니다.
        /// </summary>
        [MySQLType("VARCHAR(30)")]
        public string ChannelTitle { get; set; }

        /// <summary>
        /// 해당 동영상의 설명란을 가져옵니다.
        /// </summary>
        [MySQLType("LONGTEXT")]
        public string Description { get; set; }

        /// <summary>
        /// 해당 동영상 게시자의 ID를 가져옵니다.
        /// </summary>
        [MySQLType("VARCHAR(30)")]
        public string ChannelId { get; set; }

    }
}
