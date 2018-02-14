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
        /// 페이지의 오프셋을 결정합니다. 예를 들어 4를 입력했다면 5번째 결과부터 출력됩니다.
        /// </summary>
        int Offset { get; set; }

        /// <summary>
        /// 검색할 언어 코드를 입력합니다.
        /// </summary>
        LanguageCode Language { get; set; }

        /// <summary>
        /// 출력할 서비스들을 선택합니다.
        /// </summary>
        OutputFormat OutputServices { get; set; }
    }
}
