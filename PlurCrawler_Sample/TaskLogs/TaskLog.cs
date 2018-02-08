using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler_Sample.TaskLogs
{
    public class TaskLog
    {
        public string Message { get; set; }

        public TaskLogType LogType { get; set; }

        public DateTime DateTime { get; set; }
    }
}
