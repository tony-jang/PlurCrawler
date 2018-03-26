using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Search.Base;

namespace PlurCrawler.Search.Services.Facebook
{
    public class FacebookPageSearcher : BaseSearcher<FacebookPageSearchOption, FacebookSearchResult>
    {
        public override IEnumerable<FacebookSearchResult> Search(FacebookPageSearchOption searchOption)
        {
            return null;
        }
    }
}
