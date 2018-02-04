using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Search.Base;

namespace PlurCrawler.Format.Base
{
    public abstract class BaseFormat<TResult, TReturn> : IFormat<TResult>
                                                         where TResult : ISearchResult
    {
        public abstract TReturn Formatting(TResult resultData);
    }
}
