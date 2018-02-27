using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

using PlurCrawler.Format.Common;
using PlurCrawler.Search.Base;
using PlurCrawler.Common;
using PlurCrawler.Search.Common;

namespace PlurCrawler.Search.Services.Twitter
{
    public class TwitterSearchOption : IDateSearchOption, INotifyPropertyChanged
    {
        public static TwitterSearchOption GetDefault()
        {
            return new TwitterSearchOption()
            {
                DateRange = new DateRange(),
                SearchCount = 10,
                SplitWithDate = false,
                OutputServices = OutputFormat.Json | OutputFormat.CSV,
                Offset = 0,
                Language = TwitterLanguage.All
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

        private int _offset;

        /// <summary>
        /// 페이지의 오프셋을 결정합니다. 예를 들어 4를 입력했다면 5번째 결과부터 출력됩니다.
        /// </summary>
        public int Offset
        {
            get => _offset;
            set
            {
                _offset = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Offset"));
            }
        }

        private TwitterLanguage _language;

        /// <summary>
        /// 검색할 언어 코드를 입력합니다.
        /// </summary>
        public TwitterLanguage Language
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
        
        private bool _includeRetweets;

        /// <summary>
        /// 리트윗을 포함할지에 대한 여부를 결정합니다.
        /// </summary>
        public bool IncludeRetweets
        {
            get => _includeRetweets;
            set
            {
                _includeRetweets = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("IncludeRetweets"));
            }
        }

    }
}
