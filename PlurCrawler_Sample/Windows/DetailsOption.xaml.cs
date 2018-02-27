using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using PlurCrawler.Attributes;
using PlurCrawler.Common;
using PlurCrawler.Extension;
using PlurCrawler.Format.Common;
using PlurCrawler.Search;
using PlurCrawler.Search.Common;
using PlurCrawler.Search.Services.GoogleCSE;
using PlurCrawler.Search.Services.Twitter;
using PlurCrawler.Search.Services.Youtube;
using PlurCrawler_Sample.Common;
using PlurCrawler_Sample.Controls;

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

            foreach(var itm in EnumEx.GetValues<CountryRestrictsCode>())
            {
                googleCountry.Items.Add(new ComboBoxItem()
                {
                    Content = itm.GetAttributeFromEnum<NoteAttribute>().Message,
                    Tag = itm,
                });
            }
            googleCountry.SelectedIndex = 0;
            
            foreach(var itm in EnumEx.GetValues<RegionCode>())
            {
                youtubeRegion.Items.Add(new ComboBoxItem()
                {
                    Content = itm.GetAttributeFromEnum<NoteAttribute>().Message,
                    Tag = itm,
                });
            }
            youtubeRegion.SelectedIndex = 0;
            
            twitterLang.Items.Add(new ComboBoxItem()
            {
                Content = "제한 없음",
                Tag = TwitterLanguage.All,
            });

            foreach(var itm in EnumEx.GetValues<TwitterLanguage>())
            {
                if (itm == TwitterLanguage.All)
                    continue;
                twitterLang.Items.Add(new ComboBoxItem()
                {
                    Content = itm.ToString(),
                    Tag = itm
                });
            }
            twitterLang.SelectedIndex = 0;

            LoadGoogle(SettingManager.GoogleCSESearchOption);
            LoadTwitter(SettingManager.TwitterSearchOption);
            LoadYoutube(SettingManager.YoutubeSearchOption);

            #region [  Sync With Setting  ]

            tbGoogleSearchCount.TextChanged += GoogleSettingChanged;
            rbGoogleNoSplit.Checked += GoogleSettingChanged;
            rbGoogleSplitWithDate.Checked += GoogleSettingChanged;
            tbGoogleOffset.TextChanged += GoogleSettingChanged;
            goCbOutput1.Checked += GoogleSettingChanged;
            goCbOutput2.Checked += GoogleSettingChanged;
            goCbOutput3.Checked += GoogleSettingChanged;
            goCbOutput4.Checked += GoogleSettingChanged;
            goCbOutput1.Unchecked += GoogleSettingChanged;
            goCbOutput2.Unchecked += GoogleSettingChanged;
            goCbOutput3.Unchecked += GoogleSettingChanged;
            goCbOutput4.Unchecked += GoogleSettingChanged;
            useGoogleDate.Checked += GoogleSettingChanged;
            useGoogleDate.Unchecked += GoogleSettingChanged;
            drpGoogle.DateChanged += GoogleSettingChanged;
            
            tbTwitterSearchCount.TextChanged += TwitterSettingChanged;
            rbTwitterNoSplit.Checked += TwitterSettingChanged;
            rbTwitterSplitWithDate.Checked += TwitterSettingChanged;
            tbTwitterOffset.TextChanged += TwitterSettingChanged;
            twCbOutput1.Checked += TwitterSettingChanged;
            twCbOutput2.Checked += TwitterSettingChanged;
            twCbOutput3.Checked += TwitterSettingChanged;
            twCbOutput4.Checked += TwitterSettingChanged;
            twCbOutput1.Unchecked += TwitterSettingChanged;
            twCbOutput2.Unchecked += TwitterSettingChanged;
            twCbOutput3.Unchecked += TwitterSettingChanged;
            twCbOutput4.Unchecked += TwitterSettingChanged;
            cbTwitterRetweet.Checked += TwitterSettingChanged;
            cbTwitterRetweet.Unchecked += TwitterSettingChanged;
            drpTwitter.DateChanged += TwitterSettingChanged;
            
            tbYoutubeSearchCount.TextChanged += YoutubeSettingChanged;
            rbYoutubeNoSplit.Checked += YoutubeSettingChanged;
            rbYoutubeSplitWithDate.Checked += YoutubeSettingChanged;
            ytCbOutput1.Checked += YoutubeSettingChanged;
            ytCbOutput2.Checked += YoutubeSettingChanged;
            ytCbOutput3.Checked += YoutubeSettingChanged;
            ytCbOutput4.Checked += YoutubeSettingChanged;
            ytCbOutput1.Unchecked += YoutubeSettingChanged;
            ytCbOutput2.Unchecked += YoutubeSettingChanged;
            ytCbOutput3.Unchecked += YoutubeSettingChanged;
            ytCbOutput4.Unchecked += YoutubeSettingChanged;
            useYoutubeDate.Checked += YoutubeSettingChanged;
            useYoutubeDate.Unchecked += YoutubeSettingChanged;
            drpYoutube.DateChanged += YoutubeSettingChanged;

            drpGoogle.Loaded += Drp_Loaded;
            drpTwitter.Loaded += Drp_Loaded;
            drpYoutube.Loaded += Drp_Loaded;

            googleCountry.SelectionChanged += GoogleSettingChanged;
            youtubeRegion.SelectionChanged += YoutubeSettingChanged;
            twitterLang.SelectionChanged += TwitterSettingChanged;

            #endregion
        }

        private void Drp_Loaded(object sender, RoutedEventArgs e)
        {
            if (sender is DateRangePicker drp)
            {
                drp.LimitUntil = DateTime.Now;
            }
        }

        private void GoogleSettingChanged(object sender, EventArgs e)
        {
            SettingManager.GoogleCSESearchOption = GetGoogleCSESearchOption();
        }

        private void TwitterSettingChanged(object sender, EventArgs e)
        {
            SettingManager.TwitterSearchOption = GetTwitterSearchOption();
        }

        private void YoutubeSettingChanged(object sender, EventArgs e)
        {
            SettingManager.YoutubeSearchOption = GetYoutubeSearchOption();
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

            tbGoogleOffset.Text = option.Offset.ToString();
            tbGoogleSearchCount.Text = option.SearchCount.ToString();

            goCbOutput1.IsChecked = option.OutputServices.HasFlag(OutputFormat.CSV);
            goCbOutput2.IsChecked = option.OutputServices.HasFlag(OutputFormat.Json);
            goCbOutput3.IsChecked = option.OutputServices.HasFlag(OutputFormat.MySQL);
            goCbOutput4.IsChecked = option.OutputServices.HasFlag(OutputFormat.AccessDB);

            googleCountry.SelectedIndex = (int)option.CountryCode;

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
                Offset = tbGoogleOffset.GetIntOrDefault(),
                SplitWithDate = rbGoogleSplitWithDate.IsChecked.GetValueOrDefault(),
                SearchCount = tbGoogleSearchCount.GetIntOrDefault(),
                OutputServices = CalculateService(ServiceKind.GoogleCSE),
                CountryCode = (CountryRestrictsCode)googleCountry.SelectedIndex
            };
        }

        public void LoadTwitter(TwitterSearchOption option)
        {
            if (option == null)
                option = TwitterSearchOption.GetDefault();

            drpTwitter.Since = option.DateRange.Since.GetValueOrDefault();
            drpTwitter.Until = option.DateRange.Until.GetValueOrDefault();

            tbTwitterOffset.Text = option.Offset.ToString();
            tbTwitterSearchCount.Text = option.SearchCount.ToString();

            twCbOutput1.IsChecked = option.OutputServices.HasFlag(OutputFormat.CSV);
            twCbOutput2.IsChecked = option.OutputServices.HasFlag(OutputFormat.Json);
            twCbOutput3.IsChecked = option.OutputServices.HasFlag(OutputFormat.MySQL);
            twCbOutput4.IsChecked = option.OutputServices.HasFlag(OutputFormat.AccessDB);
            
            cbTwitterRetweet.IsChecked = option.IncludeRetweets;
            if (option.Language == TwitterLanguage.All)
            {
                twitterLang.SelectedIndex = 0;
            }
            else
            {
                twitterLang.SelectedIndex = (int)option.Language + 1;
            }

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
                Offset = tbTwitterOffset.GetIntOrDefault(),
                SplitWithDate = rbTwitterSplitWithDate.IsChecked.GetValueOrDefault(),
                SearchCount = tbTwitterSearchCount.GetIntOrDefault(),
                OutputServices = CalculateService(ServiceKind.Twitter),
                IncludeRetweets = cbTwitterRetweet.IsChecked.GetValueOrDefault(),
                Language = (TwitterLanguage)((ComboBoxItem)twitterLang.SelectedItem).Tag
            };
        }


        /// <summary>
        /// 유튜브 파트에서 설정을 불러옵니다.
        /// </summary>
        /// <param name="option"></param>
        public void LoadYoutube(YoutubeSearchOption option)
        {
            if (option == null)
                option = YoutubeSearchOption.GetDefault();

            drpYoutube.Since = option.DateRange.Since.GetValueOrDefault();
            drpYoutube.Until = option.DateRange.Until.GetValueOrDefault();
            
            tbYoutubeSearchCount.Text = option.SearchCount.ToString();

            ytCbOutput1.IsChecked = option.OutputServices.HasFlag(OutputFormat.CSV);
            ytCbOutput2.IsChecked = option.OutputServices.HasFlag(OutputFormat.Json);
            ytCbOutput3.IsChecked = option.OutputServices.HasFlag(OutputFormat.MySQL);
            ytCbOutput4.IsChecked = option.OutputServices.HasFlag(OutputFormat.AccessDB);

            youtubeRegion.SelectedIndex = (int)option.RegionCode;

            useYoutubeDate.IsChecked = option.UseDateSearch;

            if (option.SplitWithDate)
                rbYoutubeSplitWithDate.IsChecked = true;
            else
                rbYoutubeNoSplit.IsChecked = true;
        }

        public YoutubeSearchOption GetYoutubeSearchOption()
        {
            return new YoutubeSearchOption()
            {
                DateRange = new DateRange(drpYoutube.Since, drpYoutube.Until),
                UseDateSearch = useYoutubeDate.IsChecked.GetValueOrDefault(),
                SplitWithDate = rbYoutubeSplitWithDate.IsChecked.GetValueOrDefault(),
                SearchCount = tbYoutubeSearchCount.GetIntOrDefault(),
                OutputServices = CalculateService(ServiceKind.Youtube),
                RegionCode = (RegionCode)((ComboBoxItem)youtubeRegion.SelectedItem).Tag
            };
        }

        private OutputFormat CalculateService(ServiceKind kind)
        {
            CheckBox[] cbs = null;
            if (kind == ServiceKind.GoogleCSE)
                cbs = new CheckBox[]{ goCbOutput1, goCbOutput2, goCbOutput3, goCbOutput4 };
            else if (kind == ServiceKind.Twitter)
                cbs = new CheckBox[] { twCbOutput1, twCbOutput2, twCbOutput3, twCbOutput4 };
            else if (kind == ServiceKind.Youtube)
                cbs = new CheckBox[] { ytCbOutput1, ytCbOutput2, ytCbOutput3, ytCbOutput4 };

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

        public void YoutubeEnableChange(bool enable)
        {
            if (enable)
            {
                svYoutube.IsEnabled = true;
                signYoutube.Visibility = Visibility.Hidden;
            }
            else
            {
                svYoutube.IsEnabled = false;
                signYoutube.Visibility = Visibility.Visible;
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Hyperlink hyper)
            {
                Process.Start(hyper.Tag.ToString());
            }
        }
    }
}
