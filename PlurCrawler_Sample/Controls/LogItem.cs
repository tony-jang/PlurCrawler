using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using PlurCrawler.Attributes;
using PlurCrawler.Extension;

using PlurCrawler_Sample.TaskLogs;

using WPFExtension;

namespace PlurCrawler_Sample.Controls
{
    public class LogItem : ListBoxItem
    {
        public LogItem()
        {
            this.Style = FindResource("LogItemStyle") as Style;
        }

        public TaskLog TaskLog
        {
            get
            {
                return new TaskLog()
                {
                    DateTime = Date,
                    LogType = TaskLogType,
                    Message = Message
                };
            }
            set
            {
                Date = value.DateTime;
                TaskLogType = value.LogType;
                Message = value.Message;
            }
        }

        public static DependencyProperty MessageProperty = DependencyHelper.Register();

        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }
        
        #region [  Date Property  ]

        public static readonly DependencyProperty DateProperty 
            = DependencyHelper.Register(new PropertyMetadata(default(DateTime), new PropertyChangedCallback(OnDatePropertyChanged)));

        private static void OnDatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LogItem itm)
            {
                itm.SetValue(DateStringPropertyKey, itm.Date.ToString("MM/dd HH:mm:ss"));
            }
        }

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
                SetValue(DateStringPropertyKey, value.ToString("MM/dd HH:mm:ss"));
            }
        }

        #endregion

        #region [  Task Log Type Property  ]

        public static readonly DependencyProperty TaskLogTypeProperty
            = DependencyHelper.Register(new PropertyMetadata(TaskLogType.System, new PropertyChangedCallback(OnTaskLogTypeChanged)));

        private static void OnTaskLogTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is LogItem itm)
            {
                itm.SetValue(TaskLogTypeStringPropertyKey, itm.TaskLogType.GetAttributeFromEnum<DescriptionAttributeAttribute>().Message);
            }
        }

        private static readonly DependencyPropertyKey TaskLogTypeStringPropertyKey =
            DependencyHelper.RegisterReadOnly(new PropertyMetadata("시스템"));

        public static readonly DependencyProperty TaskLogTypeStringProperty =
            TaskLogTypeStringPropertyKey.DependencyProperty;
        
        public TaskLogType TaskLogType
        {
            get => (TaskLogType)GetValue(TaskLogTypeProperty);
            set
            {
                SetValue(TaskLogTypeStringPropertyKey, value.GetAttributeFromEnum<DescriptionAttributeAttribute>().Message);
                SetValue(TaskLogTypeProperty, value);
            }
        }

        public string TaskLogTypeString => (string)GetValue(TaskLogTypeStringProperty);

        #endregion
    }
}
