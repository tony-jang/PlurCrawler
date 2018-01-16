using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlurCrawler.Tokens.Credentials;

namespace PlurCrawler.Tokens.Tokenizer
{
    /// <summary>
    /// 네이버의 토큰을 생성하는 클래스입니다.
    /// </summary>
    public class NaverTokenizer : BaseTokenizer
    {
        /// <summary>
        /// 네이버의 토큰 정보을 반환합니다. 네이버는 인증과정을 거치지 않습니다.
        /// </summary>
        /// <param name="credentials">네이버의 인증 정보를 저장합니다.</param>
        /// <returns></returns>
        public override IToken CredentialsCertification(ICredentials credentials)
        {
            if (credentials is NaverCredentials naverCredentials)
            {
                return new NaverToken(naverCredentials.ClientId, naverCredentials.ClientSecret);
            }
            else
            {
                throw new CredentialsTypeException($"{credentials.GetType().ToString()} 타입이 아닌 'NaverCredentials' 타입만 넣을 수 있습니다.");
            }
        }
    }
}
