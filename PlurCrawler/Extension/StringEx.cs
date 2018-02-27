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

        public static bool SaveAsFile(this string str, string fileLocation)
        {
            return SaveAsFile(str, fileLocation, Encoding.UTF8);
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

        public static bool SaveAsFile(this string str, string fileLocation, bool appendText)
        {
            try
            {
                if (appendText)
                {
                    File.AppendAllText(fileLocation, str, Encoding.UTF8);
                    return true;
                }
                else
                {
                    return SaveAsFile(str, fileLocation, Encoding.UTF8);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static bool SaveAsFile(this string str, string fileLocation, bool appendText, Encoding encoding)
        {
            try
            {
                if (appendText)
                {
                    File.AppendAllText(fileLocation, str, encoding);
                    return true;
                }
                else
                {
                    return SaveAsFile(str, fileLocation, encoding);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string[] Split(this string str, string seperator, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
        {
            return str.Split(new string[] { seperator }, stringSplitOptions);
        }
    }
}
