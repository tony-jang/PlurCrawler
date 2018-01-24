using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Tokens.OAuth.EventArg
{
    public class AuthFailedEventArgs : EventArgs
    {
        public string Message { get; }
        public AuthFailedEventArgs(string message)
        {
            this.Message = message;
        }
    }
}
