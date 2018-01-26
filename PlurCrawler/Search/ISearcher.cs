using PlurCrawler.Search.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Tokens.Tokenizer.Base;

namespace PlurCrawler.Search
{
    public interface ISearcher
    {
        bool IsVerification { get; }

        List<ISearchResult> Search(ISearchOption searchOption);
    }
}
