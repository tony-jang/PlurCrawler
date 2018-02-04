using System;
using System.Linq;
using System.Collections.Generic;

using PlurCrawler.Extension;
using PlurCrawler.Search.Base;
using PlurCrawler.Attributes;
using PlurCrawler.Search.Common;

namespace PlurCrawler.Search
{
    /// <summary>
    /// 기본 서쳐입니다.
    /// </summary>
    public abstract class BaseSearcher<TOption, TResult> where TOption : ISearchOption
                                                         where TResult : ISearchResult
    {
        /// <summary>
        /// 검색하려는 엔진이 인증되었는지에 대한 여부를 가져옵니다.
        /// </summary>
        public bool IsVerification { get; internal set; }

        /// <summary>
        /// 검색을 실시합니다.
        /// </summary>
        /// <param name="searchOption">검색 옵션입니다.</param>
        /// <returns></returns>
        public abstract IEnumerable<TResult> Search(TOption searchOption);

        /// <summary>
        /// 각 서비스에서 사용하는 언어 코드를 가져옵니다. 없을 경우 빈 문자열을 반환합니다.
        /// </summary>
        /// <param name="serviceKind">언어 코드를 가져올 서비스 종류입니다.</param>
        /// <param name="languageCode">언어 코드입니다.</param>
        /// <returns></returns>
        public string GetLanguageCode(ServiceKind serviceKind, LanguageCode languageCode)
        {
            LanguageNoteAttribute[] attr = languageCode.GetAttributesFromEnum<LanguageNoteAttribute>();

            LanguageNoteAttribute itm = attr.Where(i => i.ServiceKind == serviceKind).FirstOrDefault();

            if (itm == null)
                return "";
            else
                return itm.LanguageString;
        }

        public delegate void SearchProgressChangedDelegate(ProgressEventArgs args);

        public event SearchProgressChangedDelegate SearchProgressChanged;

        internal void OnSearchProgressChanged(ProgressEventArgs args)
        {
            SearchProgressChanged?.Invoke(args);
        }

    }
}
