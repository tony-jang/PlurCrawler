using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using WPFExtension;

namespace PlurCrawler_Sample.Controls
{
    class ImageCheckBox : CheckBox
    {
        public ImageCheckBox()
        {
            this.Style = (Style)FindResource("ImageCheckBoxStyle");
        }
        public static DependencyProperty SourceProperty = DependencyHelper.Register();

        public static DependencyProperty DisabledSourceProperty = DependencyHelper.Register();

        public ImageSource Source
        {
            get => (ImageSource)GetValue(SourceProperty);
            set => SetValue(SourceProperty, value);
        }

        public ImageSource DisabledSource
        {
            get => (ImageSource)GetValue(DisabledSourceProperty);
            set => SetValue(DisabledSourceProperty, value);
        }
    }
}
