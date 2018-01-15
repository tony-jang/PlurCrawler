using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Tokens.Tokenizer
{
    /// <summary>
    /// 네이버 API의 토큰을 저장하고 있습니다.
    /// </summary>
    public class NaverToken : IToken
    {
        /// <summary>
        /// 네이버 API 토큰 클래스를 초기화합니다.
        /// </summary>
        /// <param name="clientId">클라이언트 ID 입니다.</param>
        /// <param name="clientSecret">클라이언트 시크릿입니다.</param>
        public NaverToken(string clientId, string clientSecret)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
        }

        /// <summary>
        /// 클라이언트 ID 입니다.
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// 클라이언트 시크릿입니다.
        /// </summary>
        public string ClientSecret { get; }
    }
}
