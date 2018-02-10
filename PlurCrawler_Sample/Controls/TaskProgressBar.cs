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
    [TemplatePart(Name = "pb", Type = typeof(ProgressBar))]
    [TemplatePart(Name = "closeBtn", Type = typeof(PathButton))]
    public class TaskProgressBar : ListViewItem
    {
        public TaskProgressBar()
        {
            this.Style = (Style)FindResource("TaskProgressBarStyle");
        }

        ProgressBar pb;
        PathButton btn;

        bool init = false;

        double savedValue, savedMaximum, savedMinimum;
        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            pb = base.GetTemplateChild("pb") as ProgressBar;
            btn = base.GetTemplateChild("closeBtn") as PathButton;

            btn.Click += Btn_Click;

            init = true;

            if (savedValue != default(double))
                Value = savedValue;

            if (savedMaximum != default(double))
                Maximum = savedMaximum;
        }

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Parent is ItemsControl lv)
            {
                lv.Items.Remove(this);
            }
        }

        public static DependencyProperty TitleProperty = DependencyHelper.Register();

        public static DependencyProperty MessageProperty = DependencyHelper.Register();

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
        
        public string Message
        {
            get => (string)GetValue(MessageProperty);
            set => SetValue(MessageProperty, value);
        }

        public double Value
        {
            get => pb.Value;
            set
            {
                if (!init)
                {
                    savedValue = value;
                    return;
                }
                pb.Value = value;
            }
        }

        public double Maximum
        {
            get => pb.Maximum;
            set
            {
                if (!init)
                {
                    savedMaximum = value;
                    return;
                }
                pb.Maximum = value;
            }
        }

        public double Minimum
        {
            get => pb.Minimum;
            set
            {
                if (!init)
                {
                    savedMinimum = value;
                    return;
                }
                pb.Minimum = value;
            }
        }
    }
}
