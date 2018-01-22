using System;

using PlurCrawler.Tokens.Credentials;
using PlurCrawler.Tokens.Tokenizer.Base;

using ti = Tweetinvi;
using tiModels = Tweetinvi.Models;

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
                    var userCredentials = ti.AuthFlow.CreateCredentialsFromVerifierCode(twitterCredentials.PINNumber, _context);
                    ti.Auth.SetCredentials(userCredentials);

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

        private tiModels.IAuthenticationContext _context;

        /// <summary>
        /// PIN 입력용 URL을 제공합니다.
        /// </summary>
        /// <returns></returns>
        public string GetURL(TwitterCredentials credentials)
        {
            if (string.IsNullOrEmpty(credentials.ConsumerKey) || string.IsNullOrEmpty(credentials.ConsumerSecret))
                return null;
            try
            {
                var appCredentials = new tiModels.TwitterCredentials(credentials.ConsumerKey, credentials.ConsumerSecret);
                _context = ti.AuthFlow.InitAuthentication(appCredentials);

                return _context.AuthorizationURL;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
