using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlurCrawler.Format.Common;
using PlurCrawler.Search.Base;

namespace PlurCrawler.Search.Services.Facebook
{
    public class FacebookPageSearchOption : ISearchOption
    {        
        public int SearchCount { get; set; }

        public OutputFormat OutputServices { get; set; }
    }
}
