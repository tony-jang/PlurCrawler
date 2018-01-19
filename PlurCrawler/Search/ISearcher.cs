using PlurCrawler.Search.SearchResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Tokens.Tokenizer.Base;
using PlurCrawler.Search.Options.Base;

namespace PlurCrawler.Search
{
    public interface ISearcher
    {
        IToken Token { get; }

        List<ISearchResult> Search(ISearchOption searchOption);
    }
}
