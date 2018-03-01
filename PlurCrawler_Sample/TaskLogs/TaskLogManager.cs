using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using PlurCrawler.Attributes;
using PlurCrawler.Extension;

namespace PlurCrawler_Sample.TaskLogs
{
    public static class TaskLogManager
    {
        public delegate void LogDelegate(TaskLog taskLog);

        public static event LogDelegate LogAdded;

        static StreamWriter sw;

        public static void Init()
        {
        }

        static TaskLogManager()
        {
            sw = new StreamWriter("Log.txt", true);
        }

        private static void OnLogAdded(TaskLog taskLog)
        {
            LogAdded?.Invoke(taskLog);
        }
        
        public static void AddLog(string message, TaskLogType type)
        {
            OnLogAdded(new TaskLog()
            {
                DateTime = DateTime.Now,
                LogType = type,
                Message = message
            });

            sw.WriteLine($"[{DateTime.Now.ToString("MM/dd HH:mm:ss")}] [{type.GetAttributeFromEnum<DescriptionAttribute>().Description}] {message}");
            sw.Flush();
        }
    }
}
