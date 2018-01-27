namespace PlurCrawler.Search.Base
{
    public interface ISearchOption
    {
        string Query { get; set; }

        ulong SearchCount { get; set; }

        ulong Offset { get; set; }

        LanguageCode Language { get; set; }
    }
}

/*

 
*/
