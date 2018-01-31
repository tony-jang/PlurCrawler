using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Search.Base;

namespace PlurCrawler.Search.Services.Twitter
{
    public class TwitterSearchResult : ISearchResult
    {
        /// <summary>
        /// 해당 트윗을 올린 날짜를 가져옵니다.
        /// </summary>
        public DateTime? PublishedDate { get; set; }

        /// <summary>
        /// 원본 URL을 가져옵니다.
        /// </summary>
        public string OriginalURL { get; set; }

        /// <summary>
        /// 올린 사람의 닉네임과 내용 10자로 이루어진 제목입니다.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 해당 트윗을 올린 사람의 이름을 나타냅니다.
        /// </summary>
        public string CreatorName { get; set; }

        /// <summary>
        /// 해당 트윗을 올린 사람의 Id를 나타냅니다.
        /// </summary>
        public string CreatorId { get; set; }

        /// <summary>
        /// 해당 트윗이 리트윗 되었는지에 대한 여부를 가져옵니다.
        /// </summary>
        public bool IsRetweeted { get; set; }

        /// <summary>
        /// 해당 트윗의 텍스트를 가져옵니다.
        /// </summary>
        public string Text { get; set; }
    }
}
