using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PlurCrawler_Sample.Extension
{
    public static class ComboBoxEx
    {
        public static int GetInt(this ComboBox comboBox)
        {
            object o = ((ComboBoxItem)comboBox.SelectedItem).Tag;

            try
            {
                int.TryParse(o.ToString(), out int i);
                return i;
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
