using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace PlurCrawler.Common
{
    /// <summary>
    /// 날짜의 시작지점과 끝 지점을 지정합니다.
    /// </summary>
    public struct DateRange : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 날짜 범위 클래스를 초기화합니다.
        /// </summary>
        /// <param name="since">시작 지점의 날짜입니다.</param>
        /// <param name="until">종료 지점의 날짜입니다.</param>
        public DateRange(DateTime? since, DateTime? until)
        {
            _since = since;
            _until = until;

            PropertyChanged = null;
        }

        private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(sender, e);
        }

        private DateTime? _since;

        /// <summary>
        /// 시작 지점의 날짜입니다.
        /// </summary>
        public DateTime? Since
        {
            get => _since;
            set
            {
                _since = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Since"));
            }
        }
        
        private DateTime? _until;
        
        /// <summary>
        /// 종료 지점의 날짜입니다.
        /// </summary>
        public DateTime? Until
        {
            get => _until;
            set
            {
                _until = value;
                OnPropertyChanged(this, new PropertyChangedEventArgs("Until"));
            }
        }

        /// <summary>
        /// 올바른 범위 인지를 나타냅니다.
        /// </summary>
        public bool Vaild => Since <= Until;

        /// <summary>
        /// 시작 날짜와 끝 날짜를 모두 <see cref="IEnumerable{T}"/>로 반환합니다.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<DateTime> GetDateRange()
        {
            var list = new List<DateTime>();
            DateTime startDate = Since.GetValueOrDefault().Date;

            while (Until.GetValueOrDefault().Date >= startDate)
            {
                list.Add(startDate);
                startDate = startDate.AddDays(1);
            }

            return list;
        }
    }
}
