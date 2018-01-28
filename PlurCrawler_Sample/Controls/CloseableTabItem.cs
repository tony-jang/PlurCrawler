using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

using PlurCrawler_Sample.Commands;

namespace PlurCrawler_Sample.Controls
{
    class CloseableTabItem : TabItem
    {
        public CloseableTabItem()
        {
            this.Style = (Style)FindResource("CloseableTabItemStyle");
            CommandBindings.Add(new CommandBinding(TabItemCommand.DeleteCommand, OnDelete, OnDeleteExecute));
        }

        public void OnDelete(object sender, ExecutedRoutedEventArgs e)
        {
            if (this.Parent.GetType() == typeof(TabControl))
            {
                TabControl tc = (TabControl)this.Parent;
                tc.Items.Remove(this);
            }
        }

        private void OnDeleteExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
    }
}
