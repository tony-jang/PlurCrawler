using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.SearchResults
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISearchResult
    {
        DateTime Date { get; set; }
        
        string OriginalURL { get; set; }

        string Title { get; set; }
    }
}
