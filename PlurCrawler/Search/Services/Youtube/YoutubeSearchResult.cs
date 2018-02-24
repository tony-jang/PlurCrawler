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
        [AccessType("DATETIME")]
        public DateTime? PublishedDate { get; set; }

        /// <summary>
        /// 해당 동영상의 원본 URL을 가져옵니다.
        /// </summary>
        [MySQLType("TEXT")]
        [AccessType("LONGTEXT")]
        public string OriginalURL { get; set; }

        /// <summary>
        /// 해당 동영상의 제목을 가져옵니다.
        /// </summary>
        [MySQLType("VARCHAR(300)")]
        [AccessType("LONGTEXT")]
        public string Title { get; set; }

        /// <summary>
        /// 해당 동영상 게시자의 제목을 가져옵니다.
        /// </summary>
        [MySQLType("VARCHAR(30)")]
        [AccessType("CHAR(30)")]
        public string ChannelTitle { get; set; }

        /// <summary>
        /// 해당 동영상의 설명란을 가져옵니다.
        /// </summary>
        [MySQLType("LONGTEXT")]
        [AccessType("LONGTEXT")]
        public string Description { get; set; }

        [IgnoreProperty]
        public string SimplifyDescription {
            get
            {
                string text = Description.Replace(Environment.NewLine, " ");
                if (text.Length >= 50)
                {
                    return text.Substring(0, 50);
                }

                return text;
            }
        }

        /// <summary>
        /// 해당 동영상 게시자의 ID를 가져옵니다.
        /// </summary>
        [MySQLType("VARCHAR(30)")]
        [AccessType("CHAR(30)")]
        public string ChannelId { get; set; }

        /// <summary>
        /// 유튜브의 고유 비디오 ID를 가져옵니다.
        /// </summary>
        [MySQLType("VARCHAR(30)")]
        [AccessType("LONGTEXT")]
        [PrimaryKey]
        public string VideoId { get; set; }

        /// <summary>
        /// 검색을 위해 입력했던 키워드를 나타냅니다.
        /// </summary>
        [MySQLType("TEXT")]
        [AccessType("LONGTEXT")]
        public string Keyword { get; set; }
    }
}
