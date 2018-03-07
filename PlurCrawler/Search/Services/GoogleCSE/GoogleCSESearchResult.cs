using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PlurCrawler.Attributes;
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
        [MySQLType("DATETIME")]
        [AccessType("DATETIME")]
        public DateTime? PublishedDate { get; set; }

        /// <summary>
        /// 해당 검색 결과의 원본 URL을 나타냅니다.
        /// </summary>
        [PrimaryKey]
        [MySQLType("VARCHAR(500)")]
        [AccessType("LONGTEXT")]
        public string OriginalURL { get; set; }
        
        /// <summary>
        /// 해당 검색 결과의 제목을 나타냅니다.
        /// </summary>
        [MySQLType("VARCHAR(200)")]
        [AccessType("LONGTEXT")]
        public string Title { get; set; }

        /// <summary>
        /// 해당 검색 결과를 미리보기 합니다.
        /// </summary>
        [MySQLType("LONGTEXT")]
        [AccessType("LONGTEXT")]
        public string Snippet { get; set; }

        [IgnoreProperty]
        [JsonIgnore]
        public string SimplifySnippet
        {
            get
            {
                string text = Snippet.Replace(Environment.NewLine, " ");
                if (text.Length >= 50)
                {
                    return text.Substring(0, 50);
                }

                return text;
            }
        }

        /// <summary>
        /// 검색을 위해 입력했던 키워드를 나타냅니다.
        /// </summary>
        [MySQLType("TEXT")]
        [AccessType("LONGTEXT")]
        public string Keyword { get; set; }


        /// <summary>
        /// 해당 사이트의 내용입니다.
        /// </summary>
        [MySQLType("LONGTEXT")]
        [AccessType("LONGTEXT")]
        public string Content { get; set; }
    }
}