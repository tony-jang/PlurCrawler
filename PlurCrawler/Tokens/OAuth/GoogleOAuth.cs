using PlurCrawler.Resources;
using PlurCrawler.Tokens.Credentials;
using PlurCrawler.Tokens.OAuth.EventArg;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

using static PlurCrawler.Tokens.OAuth.BaseOAuth;

namespace PlurCrawler.Tokens.OAuth
{
    public class GoogleOAuth : BaseOAuth
    {
        public event EventHandler<AuthRequestEventArgs> AuthRequest;
        public event EventHandler HttpServerStarted;
        public event EventHandler HttpServerStopped;

        private void OnAuthRequest(object sender, AuthRequestEventArgs e)
        {
            AuthRequest?.Invoke(sender, e);
        }

        private void OnHttpServerStarted(object sender, EventArgs e)
        {
            HttpServerStarted?.Invoke(sender, e);
        }

        private void OnHttpServerStopped(object sender, EventArgs e)
        {
            HttpServerStopped?.Invoke(sender, e);
        }

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

        public async void Authorize()
        {
            string state = RandomDataBase64url(32);
            string codeVertifier = RandomDataBase64url(32);
            string codeChallenge = Base64UrlEncodeNoPadding(sha256(codeVertifier));

            string redirectURI = GetRedirectURI();

            var http = new HttpListener();
            http.Prefixes.Add(redirectURI);
            http.Start();
            OnHttpServerStarted(this, new EventArgs());

            string authoriztionRequest = 
                $@"{authEndpoint}?response_type=code&scope=openid%20profile&redirect_uri={Uri.EscapeDataString(redirectURI)
                }&client_id={clientID}&state={state}&code_challenge={codeChallenge}&code_challenge_method={codeChallengeMethod}";

            OnAuthRequest(this, new AuthRequestEventArgs(authoriztionRequest));

            HttpListenerContext context = await http.GetContextAsync();
            HttpListenerResponse response = context.Response;

            string responseString = ResourceManager.GetTextResource("Responses/GoogleResponseText.txt");

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;

            Stream responseOutput = response.OutputStream;

            Task responseTask = responseOutput.WriteAsync(buffer, 0, buffer.Length).ContinueWith((task) =>
            {
                responseOutput.Close();
                http.Stop();
                OnHttpServerStopped(this, new EventArgs());
            });

            if (context.Request.QueryString.Get("error") != null)
            {
                
            }

        }
    }
}
