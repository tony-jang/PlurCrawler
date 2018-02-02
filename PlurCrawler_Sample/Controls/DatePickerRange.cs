﻿using System;
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

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            dpSince = base.GetTemplateChild("dpSince") as DatePicker;
            dpUntil = base.GetTemplateChild("dpUntil") as DatePicker;


            dpSince.DisplayDateStart = new DateTime(2005, 1, 1);
            dpUntil.DisplayDateStart = new DateTime(2005, 1, 1);

            dpSince.DisplayDateEnd = DateTime.Today;
            dpUntil.DisplayDateEnd = DateTime.Today;
        }

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
