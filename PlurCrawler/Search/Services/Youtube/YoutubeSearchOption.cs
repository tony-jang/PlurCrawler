using PlurCrawler.Search.Base;
using PlurCrawler.Structure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Services.Youtube
{
    public class YoutubeSearchOption : IDateSearchOption
    {
        public string Query { get; set; }

        public ulong SearchCount { get; set; }

        public ulong Offset { get; set; }
        
        public LanguageCode Language { get; set; }

        public DateRange PublishedDateRange { get; set; }
    }
}
