using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Search.Base;

namespace PlurCrawler.Search.Services.Facebook
{
    public class FacebookSearchResult : ISearchResult
    {
        public DateTime? PublishedDate { get; set; }

        public string OriginalURL { get; set; }

        public string Title { get; set; }

        public string Keyword { get; set; }
    }
}
