using PlurCrawler.Format.Common;

namespace PlurCrawler.Search.Base
{
    /// <summary>
    /// 검색 옵션입니다.
    /// </summary>
    public interface ISearchOption
    {
        /// <summary>
        /// 검색할 검색어입니다.
        /// </summary>
        string Query { get; set; }

        /// <summary>
        /// 검색할 갯수입니다.
        /// </summary>
        int SearchCount { get; set; }

        /// <summary>
        /// 출력할 서비스들을 선택합니다.
        /// </summary>
        OutputFormat OutputServices { get; set; }
    }
}
