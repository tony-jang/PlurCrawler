using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Attributes;
using PlurCrawler.Search.Base;

namespace PlurCrawler.Search.Services.Twitter
{
    public class TwitterSearchResult : ISearchResult
    {
        /// <summary>
        /// 해당 트윗을 올린 날짜를 가져옵니다.
        /// </summary>
        [MySQLType("DATETIME")]
        public DateTime? PublishedDate { get; set; }

        /// <summary>
        /// 원본 URL을 가져옵니다.
        /// </summary>
        [MySQLType("TEXT")]
        public string OriginalURL { get; set; }

        /// <summary>
        /// 올린 사람의 닉네임과 내용 10자로 이루어진 제목입니다.
        /// </summary>
        [MySQLType("VARCHAR(30)")]
        public string Title { get; set; }

        /// <summary>
        /// 해당 트윗을 올린 사람의 이름을 나타냅니다.
        /// </summary>
        [MySQLType("VARCHAR(30)")]
        public string CreatorName { get; set; }

        /// <summary>
        /// 해당 트윗을 올린 사람의 Id를 나타냅니다.
        /// </summary>
        [MySQLType("VARCHAR(20)")]
        public string CreatorId { get; set; }

        /// <summary>
        /// 해당 트윗이 다른 사용자에 의해 리트윗 되었는지에 대한 여부를 가져옵니다.
        /// </summary>
        [MySQLType("VARCHAR(10)")]
        public bool IsRetweeted { get; set; }

        /// <summary>
        /// 해당 트윗이 리트윗 된 트윗인지에 대한 여부를 가져옵니다.
        /// </summary>
        [MySQLType("VARCHAR(10)")]
        public bool IsRetweet { get; set; }

        /// <summary>
        /// 해당 트윗의 ID를 가져옵니다.
        /// </summary>
        [PrimaryKey]    
        [MySQLType("BIGINT")]
        public long ID { get; set; }

        /// <summary>
        /// 해당 트윗의 텍스트를 가져옵니다.
        /// </summary>
        [MySQLType("LONGTEXT")]
        public string Text { get; set; }

        /// <summary>
        /// 검색을 위해 입력했던 키워드를 나타냅니다.
        /// </summary>
        [MySQLType("TEXT")]
        public string Keyword { get; set; }
    }
}
