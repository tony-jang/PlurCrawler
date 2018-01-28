using System;
using PlurCrawler.Search.Base;
using PlurCrawler.Structure;

namespace PlurCrawler.Search.Services.GoogleCSE
{
    public class GoogleCSESearchOption : IDateSearchOption
    {
        public string Query { get; set; }

        public ulong SearchCount { get; set; }

        public ulong Offset { get; set; }

        public LanguageCode Language { get; set; }

        public DateRange PublishedDateRange { get; set; }
    }
}