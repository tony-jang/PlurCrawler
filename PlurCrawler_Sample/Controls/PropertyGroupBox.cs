using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PlurCrawler_Sample.Controls
{
    public class PropertyGroupBox : GroupBox
    {
        public PropertyGroupBox()
        {
            this.Style = (Style)FindResource("PropertyGroupBoxStyle");
        }
    }
}
