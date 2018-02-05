using PlurCrawler.Search.Services.GoogleCSE;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using PlurCrawler.Common;

namespace PlurCrawler_Sample.Windows
{
    /// <summary>
    /// DetailsOption.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DetailsOption : Page
    {
        public DetailsOption()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 구글 파트에서 설정을 불러옵니다.
        /// </summary>
        /// <param name="option"></param>
        public void LoadGoogle(GoogleCSESearchOption option)
        {
            drpGoogle.Since = option.DateRange.Since.GetValueOrDefault();
            drpGoogle.Until = option.DateRange.Until.GetValueOrDefault();
            tbGooglePageOffset.Text = option.Offset.ToString();
            tbGoogleSearchCount.Text = option.SearchCount.ToString();
            if (option.SplitWithDate)
                rbGoogleSplitWithDate.IsChecked = true;
            else
                rbGoogleNoSplit.IsChecked = true;
        }

        public GoogleCSESearchOption GetGoogleCSESearchOption()
        {
            return new GoogleCSESearchOption()
            {
                DateRange = new DateRange(drpGoogle.Since, drpGoogle.Until),
                Offset = ulong.Parse(tbGooglePageOffset.Text),
                SplitWithDate = rbGoogleSplitWithDate.IsChecked.GetValueOrDefault(),
                SearchCount = ulong.Parse(tbGoogleSearchCount.Text),
                OutputServices = (PlurCrawler.Format.Common.OutputFormat)5
            };
        }

        public void GoogleEnableChange(bool enable)
        {
            if (enable)
            {
                svGoogle.IsEnabled = true;
                signGoogle.Visibility = Visibility.Hidden;
            }
            else
            {
                svGoogle.IsEnabled = false;
                signGoogle.Visibility = Visibility.Visible;
            }
        }
    }
}
