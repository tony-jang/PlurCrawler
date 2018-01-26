using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlurCrawler.Tokens.Credentials;
using PlurCrawler.Tokens.Tokenizer.Base;
using PlurCrawler.Tokens.OAuth;

namespace PlurCrawler.Tokens.Tokenizer
{
    /// <summary>
    /// 구글의 토큰을 생성하는 클래스입니다.
    /// </summary>
    public class GoogleTokenizer : ITokenizer
    {
        /// <summary>
        /// 구글의 토큰 정보을 반환합니다. OAuth 2.0 인증 과정을 거칩니다.
        /// </summary>
        /// <param name="credentials">자격 정보를 저장하고 있는 <see cref="GoogleCredentials"/>가 필요합니다.</param>
        /// <returns></returns>
        public IToken CredentialsCertification(GoogleCredentials credentials)
        {
             GoogleOAuth oauth = new GoogleOAuth(credentials);
             return null;
        }
    }
}
