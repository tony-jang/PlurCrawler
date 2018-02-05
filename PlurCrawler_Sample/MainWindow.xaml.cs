using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

using PlurCrawler;
using PlurCrawler.Search;
using PlurCrawler.Search.Services.GoogleCSE;
using PlurCrawler.Search.Services.Twitter;
using PlurCrawler.Search.Services.Youtube;
using PlurCrawler.Search.Common;
using PlurCrawler.Tokens.Credentials;
using PlurCrawler.Tokens.Tokenizer;
using PlurCrawler_Sample.Windows;
using PlurCrawler.Search.Base;
using PlurCrawler_Sample.Controls;
using PlurCrawler_Sample.Common;

using Newtonsoft.Json;

using AppSetting = PlurCrawler_Sample.Properties.Settings;

namespace PlurCrawler_Sample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        DetailsOption _detailsOption;
        VertificationManager _vertificationManager;

        public MainWindow()
        {
            InitializeComponent();

            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;

            btnSearch.Click += BtnSearch_Click;

            cbGoogleService.Checked += CheckChanged;
            cbGoogleService.Unchecked += CheckChanged;

            cbTwitterService.Checked += CheckChanged;
            cbTwitterService.Unchecked += CheckChanged;

            cbYoutubeService.Checked += CheckChanged;
            cbYoutubeService.Unchecked += CheckChanged;
            
            dict = new Dictionary<ISearcher, TaskProgressBar>();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var serializer = new ObjectSerializer<GoogleCSESearchOption>();

            AppSetting.Default.GoogleOption = serializer.Serialize(_detailsOption.GetGoogleCSESearchOption());
            AppSetting.Default.Save();
        }

        private void CheckChanged(object sender, RoutedEventArgs e)
        {
            if (cbGoogleService.IsChecked.GetValueOrDefault() ||
                cbTwitterService.IsChecked.GetValueOrDefault() ||
                cbYoutubeService.IsChecked.GetValueOrDefault())
            {
                btnSearch.IsEnabled = true;
            }
            else
            {
                btnSearch.IsEnabled = false;
            }
        }

        bool googleSearching = false,
            youtubeSearching = false,
            twitterSearching = false;


        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (cbGoogleService.IsChecked.GetValueOrDefault())
                SearchGoogle();

            if (cbTwitterService.IsChecked.GetValueOrDefault())
            {
                // Twitter Service In Here.
            }

            if (cbYoutubeService.IsChecked.GetValueOrDefault())
            {
                // Youtube Service In Here.
            }

        }

        Dictionary<ISearcher, TaskProgressBar> dict;

        public void SearchGoogle()
        {
            // 동일한 서비스는 끝나기 전까지 실행이 불가능함.
            if (googleSearching)
                return;

            googleSearching = true;
            _detailsOption.GoogleEnableChange(false);
            _vertificationManager.ChangeGoogleState(false);

            Thread thr = new Thread(() =>
            {
                var googleCSESearcher = new GoogleCSESearcher();

                string googleKey = string.Empty,
                       googleID = string.Empty;

                GoogleCSESearchOption option = null;
                Dispatcher.Invoke(() =>
                {
                    // 옵션 초기화
                    option = _detailsOption.GetGoogleCSESearchOption();
                    option.Query = tbQuery.Text;
                });

                Dispatcher.Invoke(() =>
                {
                    googleKey = _vertificationManager.GoogleAPIKey;
                    googleID = _vertificationManager.GoogleEngineID;

                    var tb = new TaskProgressBar();

                    tb.Maximum = option.SearchCount;
                    tb.Title = "Google CSE 검색";
                    tb.Message = "검색이 진행중입니다.";

                    dict[googleCSESearcher] = tb;

                    lvTask.Items.Add(tb);
                });

                googleCSESearcher.Vertification(googleKey, googleID);



                googleCSESearcher.SearchProgressChanged += GoogleCSESearcher_SearchProgressChanged;
                googleCSESearcher.SearchFinished += GoogleCSESearcher_SearchFinished;

                IEnumerable<GoogleCSESearchResult> googleResult = googleCSESearcher.Search(option);

                Dispatcher.Invoke(() => {
                    googleSearching = false;
                    _detailsOption.GoogleEnableChange(true);
                    _vertificationManager.ChangeGoogleState(true);
                });
            });

            thr.Start();
        }

        private void GoogleCSESearcher_SearchFinished(object sender)
        {
            Dispatcher.Invoke(() =>
            {
                var itm = dict[sender as ISearcher];
                itm.Value = itm.Maximum;
                itm.Message = "검색이 완료되었습니다.";
                _vertificationManager.ChangeGoogleState(Common.VerifyType.Verified);
            });
        }

        private void GoogleCSESearcher_SearchProgressChanged(object sender, ProgressEventArgs args)
        {
            Dispatcher.Invoke(() =>
            {
                var itm = dict[sender as ISearcher];
                itm.Maximum = args.Maximum;
                itm.Value = args.Value;
            });
            
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region [  Initalization  ]

            _detailsOption = (DetailsOption)frOption.Content;
            _vertificationManager = (VertificationManager)frVertManager.Content;

            #endregion

#if DEBUG


            var serializer = new ObjectSerializer<GoogleCSESearchOption>();

            GoogleCSESearchOption opt = serializer.Deserialize(AppSetting.Default.GoogleOption);

            _detailsOption.LoadGoogle(opt);

            //foreach (PropertyInfo mi in typeof(GoogleCSESearchResult).GetProperties())
            //{
            //Console.Write("test");
            //string s = (string)GetPropValue(new GoogleCSESearchResult()
            //{
            //    OriginalURL = "http://naver.com"
            //}, "OriginalURL");

            //MessageBox.Show(s);
            //}
#endif
        }
    }
}
