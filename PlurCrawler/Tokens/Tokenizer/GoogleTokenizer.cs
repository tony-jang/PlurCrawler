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
    /// 구글의 토큰을 생성하는 클래스입니다.
    /// </summary>
    public class GoogleTokenizer : BaseTokenizer
    {
        /// <summary>
        /// 구글의 토큰 정보을 반환합니다. 구글은 인증과정을 거치지 않습니다.
        /// </summary>
        /// <param name="credentials">자격 정보를 저장하고 있는 <see cref="GoogleCredentials"/>가 필요합니다.</param>
        /// <returns></returns>
        public override IToken CredentialsCertification(ICredentials credentials)
        {
            if (credentials is GoogleCredentials googleCredentials)
            {
                return new GoogleToken(googleCredentials.Key, googleCredentials.EngineID);
            }
            else
            {
                throw new CredentialsTypeException($"{credentials.GetType().ToString()} 타입이 아닌 'GoogleCredentials' 타입만 넣을 수 있습니다.");
            }
        }
    }
}
