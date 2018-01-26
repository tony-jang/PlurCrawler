using PlurCrawler.Search.Base;

namespace PlurCrawler.Search.Services.GoogleCSE
{
    public class GoogleCSESearchOption : ISearchOption
    {
        public string Query { get; set; }

        public ulong SearchCount { get; set; }

        public ulong Offset { get; set; }
    }
}