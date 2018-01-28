using System.Linq;
using System.Collections.Generic;

using PlurCrawler.Extension;
using PlurCrawler.Search.Base;
using PlurCrawler.Attributes;

namespace PlurCrawler.Search
{
    public abstract class BaseSearcher
    {
        /// <summary>
        /// 검색하려는 엔진이 인증되었는지에 대한 여부를 가져옵니다.
        /// </summary>
        public bool IsVerification { get; internal set; }

        public abstract List<ISearchResult> Search(IDateSearchOption searchOption);

        public string GetLanguageCode(ServiceKind serviceKind, LanguageCode languageCode)
        {
            LanguageNoteAttribute[] attr = languageCode.GetAttributesFromEnum<LanguageNoteAttribute>();

            LanguageNoteAttribute itm = attr.Where(i => i.ServiceKind == serviceKind).FirstOrDefault();

            if (itm == null)
                return "";
            else
                return itm.LanguageString;
        }
    }
}
