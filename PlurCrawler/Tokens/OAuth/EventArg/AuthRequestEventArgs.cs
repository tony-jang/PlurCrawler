using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PlurCrawler.Tokens.OAuth.EventArg
{
    public class AuthRequestEventArgs : EventArgs
    {
        public string URL { get; set; }
        public AuthRequestEventArgs(string url)
        {
            this.URL = url;
        }
    }
}