using PlurCrawler.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler_Sample.TaskLogs
{
    public enum TaskLogType
    {
        [Note("시스템")]
        System,
        [Note("준비")]
        SearchReady,
        [Note("검색중")]
        Searching,
        [Note("실패")]
        SearchFailed,
    }
}
