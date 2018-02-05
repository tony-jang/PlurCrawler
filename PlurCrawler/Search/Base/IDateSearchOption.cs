using PlurCrawler.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Base
{
    /// <summary>
    /// 날짜 기반 검색 옵션입니다. <see cref="ISearchOption"/>을 상속 받습니다.
    /// </summary>
    public interface IDateSearchOption : ISearchOption
    {
        /// <summary>
        /// 검색 결과가 나온 결과의 범위를 지정합니다.
        /// </summary>
        DateRange DateRange { get; set; }

        /// <summary>
        /// 날짜 별로 구분해서 검색합니다, 이 때 검색 갯수는 날짜 별로 변경됩니다. (기존: 10일 전체 30개) (이후: 1일 마다 30개)
        /// </summary>
        bool SplitWithDate { get; set; }

        /// <summary>
        /// 날짜형 검색을 사용할지에 대한 여부를 결정합니다.
        /// </summary>
        bool UseDate { get; set; }
    }
}
