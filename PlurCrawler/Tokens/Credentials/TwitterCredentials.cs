using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Tokens.Credentials
{
    /// <summary>
    /// Twitter의 인증 정보를 저장하고 있는 클래스입니다.
    /// </summary>
    public class TwitterCredentials : ICredentials
    {
        /// <summary>
        /// 컨슈머 키 입니다.
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// 컨슈머 비밀 키 입니다.
        /// </summary>
        public string ConsumerSecret { get; set; }
    }
}
