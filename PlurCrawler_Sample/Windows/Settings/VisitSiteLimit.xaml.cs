using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using PlurCrawler.Common;
using PlurCrawler.Extension;

namespace PlurCrawler_Sample.Windows.Settings
{
    /// <summary>
    /// VisitSiteLimit.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class VisitSiteLimit : Window
    {
        public VisitSiteLimit()
        {
            InitializeComponent();

            btnAddDataset.Click += BtnAddDataset_Click;
            btnRemoveDataset.Click += BtnRemoveDataset_Click;

            lvDatasets.SelectionChanged += LvDatasets_SelectionChanged;

            tbDatasetName.TextChanged += Dataset_TextChanged;
            tbDatasets.TextChanged += Dataset_TextChanged;
        }

        bool innerChange = false;

        private void Dataset_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (innerChange)
                return;

            ListViewItem itm = (ListViewItem)lvDatasets.SelectedItem;

            if (itm.Tag == null)
                itm.Tag = new Pair<string, string>();
            
            Pair<string, string> value = (Pair<string, string>)itm.Tag;
            
            value.Item1 = tbDatasetName.Text;
            value.Item2 = tbDatasets.Text;

            // tbDatasets.Text.Split("\r\n").Count()
            itm.Content = $@"[{(tbDatasetName.Text.IsNullOrEmpty() ? "입력되지 않은 이름" : tbDatasetName.Text)}]{{{
                (tbDatasets.Text.Trim().IsNullOrEmpty() ? 0 : tbDatasets.Text.Split("\r\n").Count())}개}}";

        }

        private void LvDatasets_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvDatasets.SelectedIndex == -1)
            {
                tbNoDataset.Visibility = Visibility.Visible;
                tbDatasetName.IsEnabled = false;
                tbDatasets.IsEnabled = false;
                return;
            }

            ListViewItem itm = (ListViewItem)lvDatasets.SelectedItem;

            innerChange = true;

            if (itm.Tag != null)
            {
                Pair<string, string> value = (Pair<string, string>)itm.Tag;

                tbDatasetName.Text = value.Item1;
                tbDatasets.Text = value.Item2;
            }
            else
            {
                tbDatasetName.Text = string.Empty;
                tbDatasets.Text = string.Empty;
            }

            innerChange = false; 

            tbDatasetName.IsEnabled = true;
            tbDatasets.IsEnabled = true;

            tbNoDataset.Visibility = Visibility.Hidden;
        }

        private void BtnRemoveDataset_Click(object sender, RoutedEventArgs e)
        {
            if (lvDatasets.SelectedIndex == -1)
            {
                // Message To User
            }
            else
            {
                lvDatasets.Items.RemoveAt(lvDatasets.SelectedIndex);
            }
        }

        private void BtnAddDataset_Click(object sender, RoutedEventArgs e)
        {
            lvDatasets.Items.Add(new ListViewItem()
            {
                Content = "[입력되지 않은 이름] {0개}"
            });
            if (lvDatasets.SelectedIndex == -1)
                lvDatasets.SelectedIndex = 0;
        }
    }
}
