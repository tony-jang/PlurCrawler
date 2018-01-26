using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlurCrawler.Tokens.Credentials;
using PlurCrawler.Tokens.Tokenizer.Base;

namespace PlurCrawler.Tokens.Tokenizer
{
    /// <summary>
    /// 네이버의 토큰을 생성하는 클래스입니다.
    /// </summary>
    public class NaverTokenizer : ITokenizer
    {
        /// <summary>
        /// 네이버의 토큰 정보를 기존 정보와 래핑합니다.
        /// </summary>
        /// <param name="credentials">네이버의 인증 정보를 저장하는 <see cref="NaverCredentials"/>가 필요합니다.</param>
        /// <returns></returns>
        public NaverToken WrapCredentials(NaverCredentials credentials)
        {
            return new NaverToken(credentials.ClientId, credentials.ClientSecret);
        }
    }
}
