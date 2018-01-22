using PlurCrawler.Tokens.Credentials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Tokens.OAuth
{
    public class GoogleOAuth : BaseOAuth
    {
        private GoogleCredentials _credentials;
        public GoogleOAuth(GoogleCredentials credentials)
        {
            this._credentials = credentials;
        }

        public void GetURL()
        {

        }
    }
}
