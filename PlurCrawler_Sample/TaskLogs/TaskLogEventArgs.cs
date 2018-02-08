using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler_Sample.TaskLogs
{
    public class TaskLogEventArgs : EventArgs
    {
        public TaskLogEventArgs(TaskLog taskLog)
        {
            this.TaskLog = taskLog;
        }
        public TaskLog TaskLog { get; set; }
    }
}
