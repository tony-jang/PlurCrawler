using PlurCrawler.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Base
{
    public interface IDateSearchOption : ISearchOption
    {
        DateRange PublishedDateRange { get; set; }
    }
}
