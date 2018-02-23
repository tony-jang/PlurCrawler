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
        public event EventHandler DateChanged;

        private void OnDateChanged(object sender, EventArgs e)
        {
            if (sender == dpSince)
                CheckDate(true);
            else if (sender == dpUntil)
                CheckDate(false);
            DateChanged?.Invoke(sender, e);
        }

        public void CheckDate(bool isDpSince)
        {
            if (isDpSince)
            {
                if (LimitSince != null && (dpSince.SelectedDate < LimitSince || dpSince.SelectedDate > LimitUntil))
                    dpSince.SelectedDate = LimitSince;
            }
            else
            {
                if (LimitUntil != null && (dpUntil.SelectedDate > LimitUntil || dpUntil.SelectedDate < LimitSince))
                    dpUntil.SelectedDate = LimitUntil;
            }
        }

        DatePicker dpSince, dpUntil;

        public DateRangePicker()
        {
            this.Style = (Style)FindResource("DateRangePickerStyle");
        }

        DateTime saveSince, saveUntil;

        bool templateApplied = false;

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            dpSince = base.GetTemplateChild("dpSince") as DatePicker;
            dpUntil = base.GetTemplateChild("dpUntil") as DatePicker;

            if (saveSince != default(DateTime))
                dpSince.SelectedDate = saveSince;

            if (saveUntil != default(DateTime))
                dpUntil.SelectedDate = saveUntil;

            dpSince.SelectedDateChanged += OnDateChanged;
            dpUntil.SelectedDateChanged += OnDateChanged;

            if (_limitSince != null)
            {
                CheckDate(true);
                dpSince.DisplayDateStart = _limitSince;
                dpUntil.DisplayDateStart = _limitSince;
            }

            if (_limitUntil != null)
            {
                CheckDate(false);
                dpSince.DisplayDateEnd = _limitUntil;
                dpUntil.DisplayDateEnd = _limitUntil;
            }

            templateApplied = true;
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

        private DateTime? _limitSince;

        public DateTime? LimitSince
        {
            get => _limitSince;
            set
            {
                _limitSince = value;
                if (templateApplied)
                {
                    dpSince.DisplayDateStart = _limitSince;
                    dpUntil.DisplayDateStart = _limitSince;
                }

            }
        }

        private DateTime? _limitUntil;

        public DateTime? LimitUntil
        {
            get => _limitUntil;
            set
            {
                _limitUntil = value;
                if (templateApplied)
                {
                    dpSince.DisplayDateEnd = _limitUntil;
                    dpUntil.DisplayDateEnd = _limitUntil;
                }
            }
        }
    }
}
