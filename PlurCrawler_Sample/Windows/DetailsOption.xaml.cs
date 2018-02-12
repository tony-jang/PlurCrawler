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
using PlurCrawler.Format.Common;
using PlurCrawler.Search.Services.GoogleCSE;

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
            if (option == null)
                option = GoogleCSESearchOption.GetDefault();

            drpGoogle.Since = option.DateRange.Since.GetValueOrDefault();
            drpGoogle.Until = option.DateRange.Until.GetValueOrDefault();

            tbGooglePageOffset.Text = option.Offset.ToString();
            tbGoogleSearchCount.Text = option.SearchCount.ToString();

            cbOutput1.IsChecked = option.OutputServices.HasFlag(OutputFormat.CSV);
            cbOutput2.IsChecked = option.OutputServices.HasFlag(OutputFormat.Json);
            cbOutput3.IsChecked = option.OutputServices.HasFlag(OutputFormat.MySQL);
            cbOutput4.IsChecked = option.OutputServices.HasFlag(OutputFormat.AccessDB);

            useDate.IsChecked = option.UseDateSearch;

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
                UseDateSearch = useDate.IsChecked.GetValueOrDefault(),
                Offset = (ulong)tbGooglePageOffset.GetIntOrDefault(),
                SplitWithDate = rbGoogleSplitWithDate.IsChecked.GetValueOrDefault(),
                SearchCount = (ulong)tbGoogleSearchCount.GetIntOrDefault(),
                OutputServices = CalculateService()
            };
        }

        private OutputFormat CalculateService()
        {
            CheckBox[] cbs = { cbOutput1, cbOutput2, cbOutput3, cbOutput4 };

            return (OutputFormat)cbs.Where(i => i.IsChecked.GetValueOrDefault())
                .Select(i => int.Parse(i.Tag.ToString()))
                .Sum();
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
