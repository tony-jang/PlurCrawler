using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Tokens.Credentials;

using InviModel = Tweetinvi.Models;
using Invi = Tweetinvi;

namespace PlurCrawler.Tokens.Tokenizer
{
    /// <summary>
    /// 트위터의 토큰을 생성하는 토크나이저입니다.
    /// </summary>
    class TwitterTokenizer : BaseTokenizer
    {
        //public override string CredentialsPINCertification(ICredentials credentials)
        //{
        //    if (credentials is TwitterCredentials twitterCredentials)
        //    {
        //        var appCredentials = new InviModel.TwitterCredentials(twitterCredentials.ConsumerKey, twitterCredentials.ConsumerSecret);
        //        var authenticationContext = Invi.AuthFlow.InitAuthentication(appCredentials);

        //        return authenticationContext.AuthorizationURL;
        //    }
        //    else
        //    {
        //        throw new CredentialsTypeException();
        //    }
        //}

        //public override string CredentialsRedirectCertification(ICredentials credentials)
        //{
        //    if (credentials is TwitterCredentials twitterCredentials)
        //    {
        //        // TODO: Redirect URL 확인
        //        var appCredentials = new InviModel.ConsumerCredentials(twitterCredentials.ConsumerKey, twitterCredentials.ConsumerSecret);
        //        var redirectURL = "http://" + System.Web.HttpContext.Current.Request.Url.Authority + "/Home/ValidateTwitterAuth";
        //        var context = Invi.AuthFlow.InitAuthentication(appCredentials, redirectURL);

        //        return context.AuthorizationURL;
        //    }
        //    else
        //    {
        //        throw new CredentialsTypeException();
        //    }
        //}
        public override IToken CredentialsCertification(ICredentials credentials)
        {
            if (credentials is TwitterCredentials twitterCredentials)
            {
                switch (twitterCredentials.TwitterCredentialsType)
                {
                    case TwitterCredentialsType.PIN:
                        var appCredentials = new InviModel.TwitterCredentials(twitterCredentials.ConsumerKey, twitterCredentials.ConsumerSecret);
                        var authenticationContext = Invi.AuthFlow.InitAuthentication(appCredentials);

                        return new TwitterToken(authenticationContext.AuthorizationURL);
                    case TwitterCredentialsType.RedirectURL:

                        break;
                }

                return null;
            }
            else
            {
                throw new CredentialsTypeException($"{credentials.GetType().ToString()} 타입이 아닌 'TwitterCredentials' 타입만 넣을 수 있습니다.");
            }
        }
    }
}
