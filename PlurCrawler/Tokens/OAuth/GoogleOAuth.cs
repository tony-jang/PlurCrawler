using PlurCrawler.Tokens.Credentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using static PlurCrawler.Tokens.OAuth.BaseOAuth;

namespace PlurCrawler.Tokens.OAuth
{
    public class GoogleOAuth : BaseOAuth
    {
        public event EventHandler<AuthRequestEventArgs> OnAuthRequest;

        private GoogleCredentials _credentials;

        const string clientID = "581786658708-elflankerquo1a6vsckabbhn25hclla0.apps.googleusercontent.com";
        const string clientSecret = "3f6NggMbPtrmIBpgx-MK2xXK";
        const string authEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
        const string tokenEndpoint = "https://www.googleapis.com/oauth2/v4/token";
        const string userInfoEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";

        const string codeChallengeMethod = "S256";

        public GoogleOAuth(GoogleCredentials credentials)
        {
            this._credentials = credentials;
        }

        public void Authorize()
        {
            string state = RandomDataBase64url(32);
            string codeVertifier = RandomDataBase64url(32);
            string codeChallenge = Base64UrlEncodeNoPadding(sha256(codeVertifier));

            string redirectURI = GetRedirectURI();

            var http = new HttpListener();
            http.Prefixes.Add(redirectURI);
            http.Start();

            string authoriztionRequest = 
                $@"{authEndpoint}?response_type=code&scope=openid%20profile&redirect_uri={Uri.EscapeDataString(redirectURI)
                }&client_id={clientID}&state={state}&code_challenge={codeChallenge}&code_challenge_method={codeChallengeMethod}";

            // TODO : Authorize Function 완성
            
        }
    }
}
