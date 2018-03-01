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

        /// <summary>
        /// 현재 String 데이터를 파일로 저장합니다.
        /// </summary>
        /// <param name="str">저장할 String 데이터입니다.</param>
        /// <param name="fileLocation">저장할 파일 위치입니다.</param>
        /// <param name="appendText">파일을 새로 만들지 아니면 텍스트를 추가할지에 대한 여부를 나타냅니다.</param>
        /// <returns></returns>
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

        /// <summary>
        /// 현재 String 데이터를 파일로 저장합니다.
        /// </summary>
        /// <param name="str">저장할 String 데이터입니다.</param>
        /// <param name="fileLocation">저장할 파일 위치입니다.</param>
        /// <param name="appendText">파일을 새로 만들지 아니면 텍스트를 추가할지에 대한 여부를 나타냅니다.</param>
        /// <param name="encoding">파일을 저장할 <see cref="Encoding"/> 방식을 나타냅니다.</param>
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

        /// <summary>
        /// 문자열을 seperator를 통해서 잘라낼 수 있습니다.
        /// </summary>
        /// <param name="str">자를 문자열입니다.</param>
        /// <param name="seperator">자르는 기준이 될 문자열 입니다.</param>
        /// <param name="stringSplitOptions">반환된 문자열에서 빈칸일 경우 빈칸을 포함할지에 대한 여부를 포함합니다.</param>
        public static string[] Split(this string str, string seperator, StringSplitOptions stringSplitOptions = StringSplitOptions.None)
        {
            return str.Split(new string[] { seperator }, stringSplitOptions);
        }
    }
}
