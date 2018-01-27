using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlurCrawler.Search.Base;

namespace PlurCrawler.Search.Services.Twitter
{
    public class TwitterSearcher : BaseSearcher
    {
        public new bool IsVerification => Tweetinvi.Auth.Credentials != null;

        public override List<ISearchResult> Search(IDateSearchOption searchOption)
        {
            return null;
        }
    }
}
