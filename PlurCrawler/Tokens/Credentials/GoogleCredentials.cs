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
        /// 구글 인증 정보의 키 값입니다.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 구글 인증 정보의 엔진 ID 정보입니다.
        /// </summary>
        public string EngineID { get; set; }
    }
}
