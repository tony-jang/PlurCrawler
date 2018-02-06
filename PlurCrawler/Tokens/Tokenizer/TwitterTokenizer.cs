using System;
using PlurCrawler.Extension;
using PlurCrawler.Tokens.Credentials;
using PlurCrawler.Tokens.Tokenizer.Base;
using Tweetinvi;

using tiModels = Tweetinvi.Models;

namespace PlurCrawler.Tokens.Tokenizer
{
    /// <summary>
    /// 트위터의 토큰을 생성하는 클래스입니다.
    /// </summary>
    public class TwitterTokenizer : ITokenizer
    {
        /// <summary>
        /// 인증 과정을 거친뒤 TweetInvi 내부에 Credentials을 적용시킵니다.
        /// </summary>
        /// <param name="credentials">트위터의 자격 정보입니다.</param>
        /// <returns></returns>
        public void CredentialsCertification(TwitterCredentials credentials)
        {
            var userCredentials = AuthFlow.CreateCredentialsFromVerifierCode(credentials.PINNumber, _context);

            if (userCredentials == null)
            {
                throw new CredentialsTypeException("PINNumber가 잘못 입력되었습니다.");
            }

            Auth.SetCredentials(userCredentials);
        }

        private tiModels.IAuthenticationContext _context;

        /// <summary>
        /// PIN 입력용 URL을 제공합니다. 만약 잘못 입력된 경우 Null을 반환합니다.
        /// </summary>
        /// <returns></returns>
        public string GetURL(TwitterCredentials credentials)
        {
            if (credentials.ConsumerKey.IsNullOrEmpty() || credentials.ConsumerSecret.IsNullOrEmpty())
                return null;

            try
            {
                var appCredentials = new tiModels.TwitterCredentials(credentials.ConsumerKey, credentials.ConsumerSecret);
                _context = AuthFlow.InitAuthentication(appCredentials);

                return _context.AuthorizationURL;
            }
            catch (NullReferenceException)
            {
                return string.Empty;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
