using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Extension
{
    public static class DateTimeEx
    {
        public static string To8LengthYear(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }
    }
}
