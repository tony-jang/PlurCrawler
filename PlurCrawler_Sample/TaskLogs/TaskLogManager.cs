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
        public delegate void LogAddedDelegate(object sender, TaskLog taskLog);

        public delegate void LogDeletedDelegate(object sender, TaskLog taskLog);

        // TODO : Add LogAdded/Deleted Event Handler

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
            // TODO: Implement
        }
    }
}
