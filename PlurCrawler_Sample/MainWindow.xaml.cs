using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;

using PlurCrawler.Search.Services.GoogleCSE;
using PlurCrawler.Search.Services.Twitter;
using PlurCrawler.Search.Services.Youtube;
using PlurCrawler.Search.Common;
using PlurCrawler.Search.Base;

using PlurCrawler_Sample.Windows;
using PlurCrawler_Sample.Controls;
using PlurCrawler_Sample.Common;
using PlurCrawler_Sample.Export;

using AppSetting = PlurCrawler_Sample.Properties.Settings;
using System.IO;
using System.Windows.Controls;

namespace PlurCrawler_Sample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        DetailsOption _detailsOption;
        VertificationManager _vertManager;

        public MainWindow()
        {
            InitializeComponent();

            #region [  Event Connection  ]

            this.Loaded += MainWindow_Loaded;
            this.Closing += MainWindow_Closing;

            btnSearch.Click += BtnSearch_Click;

            cbGoogleService.Checked += CheckChanged;
            cbGoogleService.Unchecked += CheckChanged;

            cbTwitterService.Checked += CheckChanged;
            cbTwitterService.Unchecked += CheckChanged;

            cbYoutubeService.Checked += CheckChanged;
            cbYoutubeService.Unchecked += CheckChanged;

            btnLog.Click += BtnLog_Click;
            btnVertManager.Click += BtnVertManager_Click;

            #endregion

            dict = new Dictionary<ISearcher, TaskProgressBar>();
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region [  Initalization  ]

            _detailsOption = (DetailsOption)frOption.Content;
            _vertManager = (VertificationManager)frVertManager.Content;

            #endregion

            #region [  Load Setting  ]

            var serializer = new ObjectSerializer<GoogleCSESearchOption>();
            GoogleCSESearchOption opt = serializer.Deserialize(AppSetting.Default.GoogleOption);

            _detailsOption.LoadGoogle(opt);
            
            // 인증 정보 불러오기

            if (!string.IsNullOrEmpty(AppSetting.Default.GoogleCredentials))
            {
                string[] credentials =
                    AppSetting.Default.GoogleCredentials.Split(new string[] { "//" }, StringSplitOptions.None);

                _vertManager.SetGoogleKey(credentials[0], credentials[1]);

                _vertManager.ChangeGoogleState(AppSetting.Default.GoogleVertified);
            }
            
            #endregion

#if DEBUG
            
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

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            #region [  Save Setting  ]

            // 옵션 저장

            var serializer = new ObjectSerializer<GoogleCSESearchOption>();
            AppSetting.Default.GoogleOption = serializer.Serialize(_detailsOption.GetGoogleCSESearchOption());


            // 인증 정보 저장

            if (!(string.IsNullOrEmpty(_vertManager.GoogleAPIKey) ||
                string.IsNullOrEmpty(_vertManager.GoogleEngineID)))
            {
                AppSetting.Default.GoogleCredentials = $"{_vertManager.GoogleAPIKey}//{_vertManager.GoogleEngineID}";
                AppSetting.Default.GoogleVertified = _vertManager.GoogleVerifyType;
            }

            AppSetting.Default.Save();

            #endregion
        }

        #region [  UI EventHandler  ]

        private void CheckChanged(object sender, RoutedEventArgs e)
        {
            if (cbGoogleService.IsChecked.GetValueOrDefault() ||
                cbTwitterService.IsChecked.GetValueOrDefault() ||
                cbYoutubeService.IsChecked.GetValueOrDefault())
                btnSearch.IsEnabled = true;
            else
                btnSearch.IsEnabled = false;
        }

        private void BtnVertManager_Click(object sender, RoutedEventArgs e)
        {
            mainTabControl.SelectedIndex = 1;
            tbSelectedName.Text = (mainTabControl.SelectedItem as TabItem).Tag.ToString();
        }

        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {
            mainTabControl.SelectedIndex = 0;
            tbSelectedName.Text = (mainTabControl.SelectedItem as TabItem).Tag.ToString();
        }

#endregion

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
            _vertManager.ChangeEditable(false);

            Thread thr = new Thread(() =>
            {
                var googleCSESearcher = new GoogleCSESearcher();

                bool isCanceled = false;

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
                    googleKey = _vertManager.GoogleAPIKey;
                    googleID = _vertManager.GoogleEngineID;

                    var tb = new TaskProgressBar()
                    {
                        Title = "Google CSE 검색",
                        Maximum = option.SearchCount,
                        Message = "검색이 진행중입니다."
                    };

                    lvTask.Items.Add(tb);
                    dict[googleCSESearcher] = tb;

                    if (option.OutputServices == PlurCrawler.Format.Common.OutputFormat.None)
                    {
                        tb.Maximum = 1;
                        tb.Message = "결과를 내보낼 위치가 없습니다.";
                        isCanceled = true;
                    }
                });

                if (!isCanceled)
                {
                    googleCSESearcher.Vertification(googleKey, googleID);

                    googleCSESearcher.SearchProgressChanged += GoogleCSESearcher_SearchProgressChanged;
                    googleCSESearcher.SearchFinished += GoogleCSESearcher_SearchFinished;

                    IEnumerable<GoogleCSESearchResult> googleResult = googleCSESearcher.Search(option);

                    ExportManager.JsonExport(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "TestFile.json"), googleResult);
                }

                Dispatcher.Invoke(() => {
                    googleSearching = false;
                    _detailsOption.GoogleEnableChange(true);
                    _vertManager.ChangeEditable(true);
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
                _vertManager.ChangeGoogleState(VerifyType.Verified);
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
    }
}
