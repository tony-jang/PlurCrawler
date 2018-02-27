using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Extension;
using PlurCrawler.Format;
using PlurCrawler.Search.Base;

using PlurCrawler_Sample.Common;
using PlurCrawler_Sample.TaskLogs;

namespace PlurCrawler_Sample.Export
{
    static class ExportManager
    {
        public static bool JsonExport<TResult>(string fileLocation, IEnumerable<TResult> searchResult, bool indented = true) 
            where TResult : ISearchResult
        {
            var overlapOption = SettingManager.ExportOptionSetting.JsonOverlapOption;

            if (File.Exists(fileLocation))
            {
                if (overlapOption == FileOverrideOption.ThrowError)
                {
                    TaskLogManager.AddLog($"'{fileLocation}' 위치가 비어있지 않습니다. Json으로 내보낼 수 없었습니다.", TaskLogType.Failed);
                    return false;
                }
                else if (overlapOption == FileOverrideOption.AppendAdditionalNumber)
                {
                    int number = 1;
                    var info = new FileInfo(fileLocation);

                    while (true)
                    {
                        string tempName = $"{Path.Combine(info.DirectoryName, info.Name)} ({number++}).json";
                        if (!File.Exists(tempName))
                        {
                            fileLocation = tempName;
                            break;
                        }
                        continue;
                    }
                }
            }
            
            var jsonFormat = new JsonFormat<TResult>(indented);

            string str = jsonFormat.FormattingData(searchResult);
            
            return str.SaveAsFile(fileLocation, (File.Exists(fileLocation) && overlapOption == FileOverrideOption.AppendContent));
        }

        public static bool CSVExport<TResult>(string fileLocation, IEnumerable<TResult> searchResult)
            where TResult : ISearchResult
        {
            var overlapOption = SettingManager.ExportOptionSetting.CSVOverlapOption;

            if (File.Exists(fileLocation))
            {
                if (overlapOption == FileOverrideOption.ThrowError)
                {
                    TaskLogManager.AddLog($"'{fileLocation}' 위치가 비어있지 않습니다. CSV로 내보낼 수 없었습니다.", TaskLogType.Failed);
                    return false;
                }
                else if (overlapOption == FileOverrideOption.AppendAdditionalNumber)
                {
                    int number = 1;
                    var info = new FileInfo(fileLocation);

                    while (true)
                    {
                        string tempName = $"{Path.Combine(info.DirectoryName, info.Name)} ({number++}).csv";
                        if (!File.Exists(tempName))
                        {
                            fileLocation = tempName;
                            break;
                        }
                        continue;
                    }
                }
            }

            var csvFormat = new CSVFormat<TResult>();

            string str = csvFormat.FormattingData(searchResult);

            return str.SaveAsFile(fileLocation, (File.Exists(fileLocation) && overlapOption == FileOverrideOption.AppendContent));
        }

        public static void MySQLExport<TResult>(IEnumerable<TResult> searchResult, MySQLFormat<TResult> mySQLFormat)
            where TResult : ISearchResult
        {
            mySQLFormat.FormattingData(searchResult);
            mySQLFormat.Dispose();
        }

        public static bool AccessDBExport<TResult>(string fileLocation,IEnumerable<TResult> searchResult)
            where TResult : ISearchResult
        {
            try
            {
                AccessDBFormat<TResult> dbFormat = new AccessDBFormat<TResult>(fileLocation);

                dbFormat.FormattingData(searchResult);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return true;
        }
    }
}
