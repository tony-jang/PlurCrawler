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
    public class SearchListBoxItem : ListBoxItem
    {
        public SearchListBoxItem(DateTime dateTime, string title, string body) : this()
        {
            Date = dateTime;
            Title = title;
            Body = body;
        }
        public SearchListBoxItem()
        {
            this.Style = FindResource("SearchListBoxItemStyle") as Style;
        }
        public static DependencyProperty TitleProperty = DependencyHelper.Register();

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }


        public static DependencyProperty BodyProperty = DependencyHelper.Register();

        public string Body
        {
            get => (string)GetValue(BodyProperty);
            set => SetValue(BodyProperty, value);
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

    }
}
