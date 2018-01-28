using PlurCrawler.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Base
{
    /// <summary>
    /// 날짜 기반 검색 옵션입니다. <see cref="ISearchOption"/>을 상속 받습니다.
    /// </summary>
    public interface IDateSearchOption : ISearchOption
    {
        DateRange PublishedDateRange { get; set; }
    }
}
