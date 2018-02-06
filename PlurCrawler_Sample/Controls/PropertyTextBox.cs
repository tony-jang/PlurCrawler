using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WPFExtension;

namespace PlurCrawler_Sample.Controls
{
    class PropertyTextBox : TextBox
    {
        public PropertyTextBox()
        {
            this.Style = (Style)FindResource("PropertyTextBoxStyle");

            this.PreviewTextInput += PropertyTextBox_PreviewTextInput;
        }

        private void PropertyTextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            if (DigitOnly)
                e.Handled = !IsTextAllowed(e.Text);
        }

        public int GetIntOrDefault()
        {
            if (string.IsNullOrEmpty(this.Text))
                return 0;

            if (int.TryParse(this.Text, out int i))
                return i;

            return 0;
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9]+");
            return !regex.IsMatch(text);
        }

        public static DependencyProperty PropertyNameProperty = DependencyHelper.Register();
        public static DependencyProperty DescriptionProperty = DependencyHelper.Register();
        public static DependencyProperty AccentNameProperty = DependencyHelper.Register(new PropertyMetadata(true));
        public static DependencyProperty DigitOnlyProperty = DependencyHelper.Register();

        public string PropertyName
        {
            get => (string)GetValue(PropertyNameProperty);
            set => SetValue(PropertyNameProperty, value);
        }

        public string Description
        {
            get => (string)GetValue(DescriptionProperty);
            set => SetValue(DescriptionProperty, value);
        }

        public bool AccentName
        {
            get => (bool)GetValue(AccentNameProperty);
            set => SetValue(AccentNameProperty, value);
        }

        public bool DigitOnly
        {
            get => (bool)GetValue(DigitOnlyProperty);
            set
            {
                SetValue(DigitOnlyProperty, value);

                string str = "[^0-9]+";
                this.Text =  Regex.Replace(this.Text, str, "");
            }
        }
    }
}
