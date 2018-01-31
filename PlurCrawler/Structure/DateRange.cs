using System;

namespace PlurCrawler.Structure
{
    /// <summary>
    /// 날짜의 시작지점과 끝 지점을 지정합니다.
    /// </summary>
    public struct DateRange
    {
        /// <summary>
        /// 날짜 범위 클래스를 초기화합니다.
        /// </summary>
        /// <param name="startTime">시작 지점의 날짜입니다.</param>
        /// <param name="endTime">종료 지점의 날짜입니다.</param>
        public DateRange(DateTime? startTime, DateTime? endTime)
        {
            Initalized = true;

            _startTime = startTime;
            _endTime = endTime;
        }

        private bool Initalized;

        private DateTime? _startTime;

        /// <summary>
        /// 시작 지점의 날짜입니다.
        /// </summary>
        public DateTime? StartTime
        {
            get => _startTime;
            set
            {
                _startTime = ((Initalized && value < EndTime) || !Initalized) ? value : _startTime;
                Initalized = true;
            }
        }

        /// <summary>
        /// 시작 지점의 날짜의 값이 null인지 여부를 가져옵니다.
        /// </summary>
        public bool IsStartTimeNull => StartTime == null;
        
        private DateTime? _endTime;
        /// <summary>
        /// 종료 지점의 날짜입니다.
        /// </summary>
        public DateTime? EndTime
        {
            get => _endTime;
            set
            {
                _endTime = ((Initalized && value > StartTime) || !Initalized) ? value : _endTime;
                Initalized = true;
            }
        }

        /// <summary>
        /// 종료 지점의 날짜의 값이 null인지 여부를 가져옵니다.
        /// </summary>
        public bool IsEndTimeNull => EndTime == null;
    }
}
