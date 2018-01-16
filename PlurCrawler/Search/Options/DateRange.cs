using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Options
{
    public struct DateRange
    {
        public DateRange(DateTime startTime, DateTime endTime)
        {
            Initalized = true;

            _startTime = startTime;
            _endTime = endTime;
        }

        private bool Initalized;

        private DateTime _startTime;
        public DateTime StartTime
        {
            get => _startTime;
            set
            {
                _startTime = ((Initalized && value < EndTime) || !Initalized) ? value : _startTime;
                Initalized = true;
            }
        }

        private DateTime _endTime;
        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                _endTime = ((Initalized && value > StartTime) || !Initalized) ? value : _endTime;
                Initalized = true;
            }
        }
    }
}
