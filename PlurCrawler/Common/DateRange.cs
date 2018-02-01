using System;

namespace PlurCrawler.Common
{
    /// <summary>
    /// 날짜의 시작지점과 끝 지점을 지정합니다.
    /// </summary>
    public struct DateRange
    {
        /// <summary>
        /// 날짜 범위 클래스를 초기화합니다.
        /// </summary>
        /// <param name="since">시작 지점의 날짜입니다.</param>
        /// <param name="until">종료 지점의 날짜입니다.</param>
        public DateRange(DateTime? since, DateTime? until)
        {
            _since = since;
            _until = until;
        }

        private DateTime? _since;

        /// <summary>
        /// 시작 지점의 날짜입니다.
        /// </summary>
        public DateTime? Since
        {
            get => _since;
            set => _since = (value < Until) ? value : _since;
        }
        
        private DateTime? _until;
        /// <summary>
        /// 종료 지점의 날짜입니다.
        /// </summary>
        public DateTime? Until
        {
            get => _until;
            set => _until = (value > Since) ? value : _until;
        }
    }
}
