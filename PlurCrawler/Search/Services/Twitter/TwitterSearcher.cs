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

        public override List<ISearchResult> Search(ISearchOption searchOption)
        {
            if (searchOption is TwitterSearchOption twitterSearchOption)
            {
                return Search(twitterSearchOption).Select(i => (ISearchResult)i).ToList();
            }
            else
            {
                throw new SearchOptionTypeException("TwitterSearchOption만 넣을 수 있습니다.");
            }
        }

        public List<TwitterSearchResult> Search(TwitterSearchOption searchOption)
        {
            return null;
        }
    }
}
