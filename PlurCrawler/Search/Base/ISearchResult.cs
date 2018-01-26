using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Base
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISearchResult
    {
        DateTime? PublishedDate { get; set; }
        
        string OriginalURL { get; set; }

        string Title { get; set; }
    }
}
