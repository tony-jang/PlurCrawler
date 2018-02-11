using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Extension;
using PlurCrawler.Format;
using PlurCrawler.Search.Base;

namespace PlurCrawler_Sample.Export
{
    static class ExportManager
    {
        public static void JsonExport<TResult>(string fileLocation, IEnumerable<TResult> searchResult) 
            where TResult : ISearchResult
        {
            var jsonFormat = new JsonFormat<TResult>();

            string str = jsonFormat.FormattingData(searchResult);
            
            str.SaveAsFile(fileLocation);
        }

        public static void CSVExport<TResult>(string fileLocation, IEnumerable<TResult> searchResult)
            where TResult : ISearchResult
        {
            var csvFormat = new CSVFormat<TResult>();

            string str = csvFormat.FormattingData(searchResult);

            str.SaveAsFile(fileLocation);
        }

        public static void MySQLExport<TResult>(string fileLocation, IEnumerable<TResult> searchResult, MySQLFormat<TResult> mySQLFormat)
            where TResult : ISearchResult
        {
            mySQLFormat.FormattingData(searchResult);
        }
    }
}
