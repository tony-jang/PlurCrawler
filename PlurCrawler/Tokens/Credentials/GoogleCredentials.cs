using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Tokens.Credentials
{
    public class GoogleCredentials : ICredentials
    {
        /// <summary>
        /// 구글 인증 정보의 클라이언트 ID입니다.
        /// </summary>
        public string ClientID { get; set; }

        /// <summary>
        /// 구글 인증 정보의 클라이언트 보안 비밀입니다.
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
