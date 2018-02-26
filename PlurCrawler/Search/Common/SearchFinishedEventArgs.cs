using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Common
{
    public class SearchFinishedEventArgs : EventArgs
    {
        public SearchFinishedEventArgs(ServiceKind serviceKind)
        {
            this.ServiceKind = serviceKind;
        }
        public ServiceKind ServiceKind { get; set; }
    }
}
