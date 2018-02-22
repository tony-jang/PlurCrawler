using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Common
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string message)
        {
            this.Message = message;
        }

        public string Message { get; set; }
    }
}
