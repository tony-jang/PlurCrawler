using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PlurCrawler_Sample.TaskLogs
{
    public class TaskLogManager
    {
        public delegate void LogDelegate(object sender, TaskLog taskLog);

        public event LogDelegate LogAdded;

        internal void OnLogAdded(object sender, TaskLog taskLog)
        {
            LogAdded?.Invoke(sender, taskLog);
        }
        
        public TaskLogManager()
        {
            // TODO: Implement
        }

        public bool FileConnected { get; internal set; }

        /// <summary>
        /// 로그 출력용 파일을 연결 시킵니다.
        /// </summary>
        /// <param name="FileName"></param>
        public void ConnectFile(string FileName)
        {
            // TODO: Implement
        }

        public void WriteLine()
        {
            // TODO: Implement
        }
        
        public void AddLog(string message, TaskLogType type)
        {
            OnLogAdded(this, new TaskLog()
            {
                DateTime = DateTime.Now,
                LogType = type,
                Message = message
            });
            // TODO: Implement
        }
    }
}
