using System.Collections.Generic;

using PlurCrawler.Search.Base;

namespace PlurCrawler.Search
{
    public interface ISearcher
    {
        bool IsVerification { get; }

        List<ISearchResult> Search(ISearchOption searchOption);
    }
}
