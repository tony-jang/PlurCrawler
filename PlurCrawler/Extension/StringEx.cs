using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Extension
{
    public static class StringEx
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// 현재 String 데이터를 파일로 저장합니다.
        /// </summary>
        /// <param name="str">저장할 String 데이터입니다.</param>
        /// <param name="fileLocation">저장할 파일 위치입니다.</param>
        /// <returns></returns>
        public static bool SaveAsFile(this string str, string fileLocation, Encoding encoding)
        {
            try
            {
                File.WriteAllText(fileLocation, str, encoding);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SaveAsFile(this string str, string fileLocation)
        {
            return SaveAsFile(str, fileLocation, Encoding.UTF8);
        }
    }
}
