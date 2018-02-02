using PlurCrawler.Search.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Format.Base
{
    public interface IFormat<TResult> where TResult : ISearchResult
    {
    }
}
