using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using PlurCrawler.Format.Common;
using PlurCrawler.Search.Base;
using PlurCrawler.Common;

namespace PlurCrawler.Search.Services.Youtube
{
    public class YoutubeSearchOption : IDateSearchOption, INotifyPropertyChanged
    {
        /// <summary>
        /// <see cref="YoutubeSearchOption"/>의 기본 설정 값을 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public static YoutubeSearchOption GetDefault()
        {
            return new YoutubeSearchOption()
            {
                DateRange = new DateRange(),
                SearchCount = 10,
                SplitWithDate = false,
                UseDateSearch = false,
                OutputServices = OutputFormat.Json,
                Language = LanguageCode.All
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

        private string _query;

        /// <summary>
        /// 검색할 검색어입니다.
        /// </summary>
        public string Query
        {
            get => _query;
            set
            {
                _query = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Query"));
            }
        }

        private int _searchCount;

        /// <summary>
        /// 검색할 갯수입니다.
        /// </summary>
        public int SearchCount
        {
            get => _searchCount;
            set
            {
                _searchCount = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("SearchCount"));
            }
        }

        private LanguageCode _language;

        /// <summary>
        /// 검색할 언어 코드를 입력합니다.
        /// </summary>
        public LanguageCode Language
        {
            get => _language;
            set
            {
                _language = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Language"));
            }
        }

        private DateRange _dateRange;

        /// <summary>
        /// 검색 결과가 나온 결과의 범위를 지정합니다.
        /// </summary>
        public DateRange DateRange
        {
            get => _dateRange;
            set
            {
                _dateRange = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("DateRange"));
            }
        }

        private bool _splitWithDate;

        /// <summary>
        /// 날짜 별로 구분해서 검색합니다, 이 때 검색 갯수는 날짜 별로 변경됩니다. (기존: 10일 전체 30개) (이후: 1일 마다 30개)
        /// </summary>
        public bool SplitWithDate
        {
            get => _splitWithDate;
            set
            {
                _splitWithDate = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("SplitWithDate"));
            }
        }

        private OutputFormat _outputServices;

        /// <summary>
        /// 출력할 서비스들을 선택합니다.
        /// </summary>
        public OutputFormat OutputServices
        {
            get => _outputServices;
            set
            {
                _outputServices = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("OutputServices"));
            }
        }

        private bool _useDateSearch;

        /// <summary>
        /// 날짜형 검색을 사용할지에 대한 여부를 결정합니다.
        /// </summary>
        public bool UseDateSearch
        {
            get => _useDateSearch;
            set
            {
                _useDateSearch = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("UseDateSearch"));
            }
        }
    }
}
