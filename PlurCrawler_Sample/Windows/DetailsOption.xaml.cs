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
using PlurCrawler.Search;
using PlurCrawler.Search.Services.GoogleCSE;
using PlurCrawler.Search.Services.Twitter;

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

            goCbOutput1.IsChecked = option.OutputServices.HasFlag(OutputFormat.CSV);
            goCbOutput2.IsChecked = option.OutputServices.HasFlag(OutputFormat.Json);
            goCbOutput3.IsChecked = option.OutputServices.HasFlag(OutputFormat.MySQL);
            goCbOutput4.IsChecked = option.OutputServices.HasFlag(OutputFormat.AccessDB);

            useGoogleDate.IsChecked = option.UseDateSearch;

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
                UseDateSearch = useGoogleDate.IsChecked.GetValueOrDefault(),
                Offset = tbGooglePageOffset.GetIntOrDefault(),
                SplitWithDate = rbGoogleSplitWithDate.IsChecked.GetValueOrDefault(),
                SearchCount = tbGoogleSearchCount.GetIntOrDefault(),
                OutputServices = CalculateService(ServiceKind.GoogleCSE),
            };
        }

        public void LoadTwitter(TwitterSearchOption option)
        {
            if (option == null)
                option = TwitterSearchOption.GetDefault();

            drpTwitter.Since = option.DateRange.Since.GetValueOrDefault();
            drpTwitter.Until = option.DateRange.Until.GetValueOrDefault();

            tbTwitterPageOffset.Text = option.Offset.ToString();
            tbTwitterSearchCount.Text = option.SearchCount.ToString();

            twCbOutput1.IsChecked = option.OutputServices.HasFlag(OutputFormat.CSV);
            twCbOutput2.IsChecked = option.OutputServices.HasFlag(OutputFormat.Json);
            twCbOutput3.IsChecked = option.OutputServices.HasFlag(OutputFormat.MySQL);
            twCbOutput4.IsChecked = option.OutputServices.HasFlag(OutputFormat.AccessDB);

            useTwitterDate.IsChecked = option.UseDateSearch;

            if (option.SplitWithDate)
                rbTwitterSplitWithDate.IsChecked = true;
            else
                rbTwitterNoSplit.IsChecked = true;
        }

        public TwitterSearchOption GetTwitterSearchOption()
        {
            return new TwitterSearchOption()
            {
                DateRange = new DateRange(drpTwitter.Since, drpTwitter.Until),
                UseDateSearch = useTwitterDate.IsChecked.GetValueOrDefault(),
                Offset = tbTwitterPageOffset.GetIntOrDefault(),
                SplitWithDate = rbTwitterSplitWithDate.IsChecked.GetValueOrDefault(),
                SearchCount = tbTwitterSearchCount.GetIntOrDefault(),
                OutputServices = CalculateService(ServiceKind.Twitter),
            };

        }

        private OutputFormat CalculateService(ServiceKind kind)
        {
            CheckBox[] cbs = null;
            if (kind == ServiceKind.GoogleCSE)
                cbs = new CheckBox[]{ goCbOutput1, goCbOutput2, goCbOutput3, goCbOutput4 };
            else if (kind == ServiceKind.Twitter)
                cbs = new CheckBox[] { twCbOutput1, twCbOutput2, twCbOutput3, twCbOutput4 };

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

        public void TwitterEnableChange(bool enable)
        {
            if (enable)
            {
                svTwitter.IsEnabled = true;
                signTwitter.Visibility = Visibility.Hidden;
            }
            else
            {
                svTwitter.IsEnabled = false;
                signTwitter.Visibility = Visibility.Visible;
            }
        }
    }
}
