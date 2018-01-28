using Dragablz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler_Sample
{
    class MainWindowViewModel
    {
        public IInterTabClient InterTabClient { get; set; }
        public MainWindowViewModel()
        {
            InterTabClient = new MainInterTabClient();
        }
    }
}
