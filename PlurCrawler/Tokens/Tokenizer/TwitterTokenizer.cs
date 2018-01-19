using System;

using PlurCrawler.Tokens.Credentials;
using PlurCrawler.Tokens.Tokenizer.Base;

using Tweetinvi;

namespace PlurCrawler.Tokens.Tokenizer
{
    /// <summary>
    /// 트위터의 토큰을 생성하는 클래스입니다.
    /// </summary>
    public class TwitterTokenizer : BaseTokenizer
    {
        /// <summary>
        /// 인증 과정을 거칩니다. Twitter의 경우 Token을 반환하지 않습니다. (TweetInvi 참고)
        /// </summary>
        /// <param name="credentials">트위터의 자격 정보입니다.</param>
        /// <returns></returns>
        public override IToken CredentialsCertification(ICredentials credentials)
        {
            if (credentials is TwitterCredentials twitterCredentials)
            {
                try
                {
                    var userCredentials = AuthFlow.CreateCredentialsFromVerifierCode(twitterCredentials.PINNumber, twitterCredentials.Context);

                    Auth.SetCredentials(userCredentials);

                    return null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            else
            {
                throw new CredentialsTypeException($"{credentials.GetType().ToString()} 타입이 아닌 'TwitterCredentials' 타입만 넣을 수 있습니다.");
            }
        }
    }
}
