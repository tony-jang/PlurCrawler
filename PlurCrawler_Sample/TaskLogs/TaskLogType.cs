using PlurCrawler.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler_Sample.TaskLogs
{
    public enum TaskLogType
    {
        [Description("시스템")]
        System,
        [Description("준비")]
        SearchReady,
        [Description("검색중")]
        Searching,
        [Description("실패")]
        Failed,
        [Description("완료")]
        Complete,
    }
}
