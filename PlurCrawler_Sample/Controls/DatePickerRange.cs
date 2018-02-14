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
    [TemplatePart(Name = "dpSince", Type = typeof(DatePicker))]
    [TemplatePart(Name = "dpUntil", Type = typeof(DatePicker))]
    class DateRangePicker : Control
    {
        DatePicker dpSince, dpUntil;

        public DateRangePicker()
        {
            this.Style = (Style)FindResource("DateRangePickerStyle");
        }

        DateTime saveSince, saveUntil;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            dpSince = base.GetTemplateChild("dpSince") as DatePicker;
            dpUntil = base.GetTemplateChild("dpUntil") as DatePicker;

            if (saveSince != default(DateTime))
                dpSince.SelectedDate = saveSince;

            if (saveUntil != default(DateTime))
                dpUntil.SelectedDate = saveUntil;

            dpSince.SelectedDateChanged += DpSince_SelectedDateChanged;
        }

        private void DpSince_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            MessageBox.Show(Since.ToLongDateString());
        }

        public static DependencyProperty PropertyNameProperty = DependencyHelper.Register();

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }

        public DateTime Since
        {
            get => dpSince.SelectedDate.GetValueOrDefault();
            set
            {
                if (dpSince == null)
                    saveSince = value;
                else
                    dpSince.SelectedDate = value;
            }
        }

        public DateTime Until
        {
            get => dpUntil.SelectedDate.GetValueOrDefault();
            set
            {
                if (dpUntil == null)
                    saveUntil = value;
                else
                    dpUntil.SelectedDate = value;
            }
        }
    }
}
