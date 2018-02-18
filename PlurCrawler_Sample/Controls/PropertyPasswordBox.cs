using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

using PlurCrawler.Extension;

using WPFExtension;

namespace PlurCrawler_Sample.Controls
{
    [TemplatePart(Name ="pwBox", Type =typeof(PasswordBox))]
    public class PropertyPasswordBox : Control
    {
        public PropertyPasswordBox()
        {
            this.Style = (Style)FindResource("PropertyPasswordBoxStyle");
        }

        public event EventHandler PasswordChanged;

        private void OnPasswordChanged(object sender, EventArgs e)
        {
            PasswordChanged?.Invoke(sender, e);
        }

        PasswordBox pwBox;
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            pwBox = base.GetTemplateChild("pwBox") as PasswordBox;

            pwBox.PasswordChanged += OnPasswordChanged;

            if (!savedPassword.IsNullOrEmpty())
            {
                pwBox.Password = savedPassword;
            }
        }

        private string savedPassword;

        public static DependencyProperty PropertyNameProperty = DependencyHelper.Register();
        public static DependencyProperty DescriptionProperty = DependencyHelper.Register();
        public static DependencyProperty AccentNameProperty = DependencyHelper.Register(new PropertyMetadata(true));
        
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

        public string Password
        {
            get => pwBox?.Password;
            set
            {
                if (pwBox == null)
                {
                    savedPassword = value;
                    return;
                }
                pwBox.Password = value;
            }
        }
    }
}
