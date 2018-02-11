using PlurCrawler.Search.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Format.Base
{
    public abstract class BaseFormat<TResult> : IFormat<TResult>
                                                where TResult : ISearchResult
    {
        public abstract void FormattingData(IEnumerable<TResult> resultData);
    }
}
