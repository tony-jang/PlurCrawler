using PlurCrawler.Search.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Services.Twitter
{
    public class TwitterSearchOption : IDateSearchOption
    {
        public DateTime? PublishedAfter { get; set; }

        public DateTime? PublishedBefore { get; set; }

        public string Query { get; set; }

        public ulong SearchCount { get; set; }

        public ulong Offset { get; set; }
        public LanguageCode Language { get; set; }
    }
}
