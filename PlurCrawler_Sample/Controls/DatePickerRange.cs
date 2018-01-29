using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

using WPFExtension;

namespace PlurCrawler_Sample.Controls
{
    class DatePickerRange : Control
    {
        public static DependencyProperty PropertyNameProperty = DependencyHelper.Register();
        public static DependencyProperty StartDateTimeProperty = DependencyHelper.Register();
        public static DependencyProperty EndDateTimeProperty = DependencyHelper.Register();

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }

        public DateTime StartDateTime
        {
            get => (DateTime)GetValue(StartDateTimeProperty);
            set => SetValue(StartDateTimeProperty, value);
        }

        public DateTime EndDateTime
        {
            get => (DateTime)GetValue(EndDateTimeProperty);
            set => SetValue(EndDateTimeProperty, value);
        }
    }
}
