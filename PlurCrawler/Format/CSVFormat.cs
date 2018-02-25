using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PlurCrawler.Attributes;
using PlurCrawler.Extension;
using PlurCrawler.Format.Base;
using PlurCrawler.Search.Base;

namespace PlurCrawler.Format
{
    public class CSVFormat<TResult> : BaseFormat<TResult, string> where TResult : ISearchResult
    {
        public override string FormattingData(IEnumerable<TResult> resultData)
        {
            if (resultData == null ||
                resultData.Count() == 0)
            {
                return "";
            }
            
            StringBuilder sb = new StringBuilder();

            // 프로퍼티 목록 추출
            IEnumerable<string> properties = resultData
                .First()
                .GetType()
                .GetProperties()
                .Where(i => i.GetCustomAttributes<IgnorePropertyAttribute>().Count() == 0)
                .Select(i => i.Name);

            sb.AppendLine(string.Join(",", properties));

            foreach(TResult item in resultData)
            {
                foreach (string property in properties)
                {
                    object obj = GetPropValue(item, property);

                    string data = DeterminationType(obj);
                    if (!data.IsNullOrEmpty())
                        sb.Append($"\"{data}\",");
                    else
                        sb.Append(",");
                }
                sb.Append(Environment.NewLine);
            }
            
            return sb.ToString();
        }
        
        private static string DeterminationType(object obj)
        {
            if (obj == null)
                return "";

            if (obj is string str)
            {
                return str.Replace("\"", "\"\"").Replace("\r", "\\r").Replace("\n", "\\n");
            }
            else if (obj is int || obj is double || obj is uint || obj is ulong)
            {
                return obj.ToString();
            }
            else if (obj is DateTime dt)
            {
                return obj.ToString();
            }
            else if (obj is bool b)
            {
                return b ? "True" : "False";
            }
            
            return "";
        }
        private static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src);
        }
    }
}
