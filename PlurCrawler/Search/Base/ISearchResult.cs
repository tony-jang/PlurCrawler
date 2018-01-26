using System;

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
