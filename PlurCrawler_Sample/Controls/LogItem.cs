using PlurCrawler.Attributes;
using PlurCrawler.Extension;
using PlurCrawler_Sample.TaskLog;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFExtension;

namespace PlurCrawler_Sample.Controls
{
    public class LogItem : ListBoxItem
    {
        public LogItem()
        {
            this.Style = FindResource("LogItemStyle") as Style;

            Date = DateTime.Now;
            TaskLogType = TaskLogType.SearchFailed;
            Message = "검색 설정에 실패했습니다. 자세한 내용은 여기를 확인해주세요,";
        }

        public static DependencyProperty MessageProperty = DependencyHelper.Register();

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }
        

        #region [  Date Property  ]

        public static readonly DependencyProperty DateProperty 
            = DependencyHelper.Register();
        
        private static readonly DependencyPropertyKey DateStringPropertyKey =
            DependencyHelper.RegisterReadOnly(new PropertyMetadata(""));

        public static readonly DependencyProperty DateStringProperty =
            DateStringPropertyKey.DependencyProperty;

        public string DateString
        {
            get => (string)GetValue(DateStringProperty);
        }
        
        public DateTime Date
        {
            get => (DateTime)GetValue(DateProperty);
            set
            {
                SetValue(DateProperty, value);
                SetValue(DateStringPropertyKey, value.ToString("MM/dd HH:mm"));
            }
        }

        #endregion

        #region [  Task Log Type Property  ]

        public static readonly DependencyProperty TaskLogTypeProperty
            = DependencyHelper.Register();

        private static readonly DependencyPropertyKey TaskLogTypeStringPropertyKey =
            DependencyHelper.RegisterReadOnly(new PropertyMetadata(""));

        public static readonly DependencyProperty TaskLogTypeStringProperty =
            TaskLogTypeStringPropertyKey.DependencyProperty;
        
        public TaskLogType TaskLogType
        {
            get => (TaskLogType)GetValue(TaskLogTypeProperty);
            set
            {
                SetValue(TaskLogTypeStringPropertyKey, value.GetAttributeFromEnum<NoteAttribute>().Message);
                SetValue(TaskLogTypeProperty, value);
            }
        }

        public string TaskLogTypeString => (string)GetValue(TaskLogTypeStringProperty);

        #endregion
    }
}
