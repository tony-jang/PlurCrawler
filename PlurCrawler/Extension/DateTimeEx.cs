using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Extension
{
    public static class DateTimeEx
    {
        /// <summary>
        /// DateTime을 yyyyMMdd 포맷으로 반환합니다.
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string To8LengthYear(this DateTime dateTime)
        {
            return dateTime.ToString("yyyyMMdd");
        }
    }
}
