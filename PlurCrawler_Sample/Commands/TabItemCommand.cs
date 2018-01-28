using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PlurCrawler_Sample.Commands
{
    static class TabItemCommand
    {
        public static RoutedCommand DeleteCommand { get; }

        static TabItemCommand()
        {
            DeleteCommand = new RoutedCommand("Delete", typeof(TabItemCommand));
        }
    }
}
