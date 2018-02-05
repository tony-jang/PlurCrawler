using System;
using PlurCrawler.Search.Base;
using PlurCrawler.Common;
using PlurCrawler.Format.Common;
using System.Xml.Serialization;
using System.ComponentModel;

namespace PlurCrawler.Search.Services.GoogleCSE
{
    public class GoogleCSESearchOption : IDateSearchOption
    {
        /// <summary>
        /// 검색할 검색어입니다.
        /// </summary>
        public string Query { get; set; }

        /// <summary>
        /// 검색할 갯수입니다.
        /// </summary>
        public ulong SearchCount { get; set; }

        /// <summary>
        /// 페이지의 오프셋을 결정합니다. 예를 들어 4를 입력했다면 5번째 결과부터 출력됩니다.
        /// </summary>
        public ulong Offset { get; set; }

        /// <summary>
        /// 검색할 언어 코드를 입력합니다.
        /// </summary>
        public LanguageCode Language { get; set; }

        /// <summary>
        /// 검색 결과가 나온 결과의 범위를 지정합니다.
        /// </summary>
        public DateRange DateRange { get; set; }

        /// <summary>
        /// 날짜 별로 구분해서 검색합니다, 이 때 검색 갯수는 날짜 별로 변경됩니다. (기존: 10일 전체 30개) (이후: 1일 마다 30개)
        /// </summary>
        public bool SplitWithDate { get; set; }

        /// <summary>
        /// 출력할 서비스들을 선택합니다.
        /// </summary>
        [XmlIgnore]
        public OutputFormat OutputServices { get; set; }

        [XmlElement("OutputServices")]
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public int OutputServicesInt
        {
            get => (int)OutputServices;
            set => OutputServices = (OutputFormat)value;
        }

        /// <summary>
        /// 날짜형 검색을 사용할지에 대한 여부를 결정합니다.
        /// </summary>
        public bool UseDate { get; set; }
    }
}
