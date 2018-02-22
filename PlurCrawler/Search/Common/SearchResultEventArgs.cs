using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Search.Base;

namespace PlurCrawler.Search.Common
{
    public class SearchResultEventArgs : EventArgs
    {
        public ISearchResult Result { get; set; }

        public ServiceKind Kind { get; set; }

        public SearchResultEventArgs(ISearchResult result, ServiceKind kind)
        {
            this.Result = result;
            this.Kind = kind;
        }
    }
}
