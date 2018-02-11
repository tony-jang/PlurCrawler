using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows;
using System.Windows.Controls;

using PlurCrawler.Search.Services.GoogleCSE;
using PlurCrawler.Search.Services.Twitter;
using PlurCrawler.Search.Services.Youtube;
using PlurCrawler.Search.Common;
using PlurCrawler.Search.Base;
using PlurCrawler.Extension;
using PlurCrawler.Format;

using PlurCrawler_Sample.Windows;
using PlurCrawler_Sample.Controls;
using PlurCrawler_Sample.Common;
using PlurCrawler_Sample.TaskLogs;

using AppSetting = PlurCrawler_Sample.Properties.Settings;

namespace PlurCrawler_Sample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        DetailsOption _detailsOption;
        VertificationManager _vertManager;
        TaskLogManager _logManager;
        
        Dictionary<ISearcher, TaskProgressBar> dict;

        public MainWindow()
        {
            InitializeComponent();

            #region [  Initalization  ]
            
            _logManager = new TaskLogManager();

            dict = new Dictionary<ISearcher, TaskProgressBar>();

            #endregion

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
            btnTaskReport.Click += BtnTaskReport_Click;

            _logManager.LogAdded += _logManager_LogAdded;

            #endregion
        }

        private void _logManager_LogAdded(object sender, TaskLog taskLog)
        {
            Dispatcher.Invoke(() =>
            {
                lvLog.Items.Add(new LogItem()
                {
                    TaskLog = taskLog
                });
            });
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region [  Initalization UI  ]

            lvLog.Items.Clear();

            _detailsOption = (DetailsOption)frOption.Content;
            _vertManager = (VertificationManager)frVertManager.Content;
            
            #endregion

            #region [  Load Setting  ]

            var serializer = new ObjectSerializer<GoogleCSESearchOption>();
            GoogleCSESearchOption opt = serializer.Deserialize(AppSetting.Default.GoogleOption);

            _detailsOption.LoadGoogle(opt);
            
            // 인증 정보 불러오기

            if (!AppSetting.Default.GoogleCredentials.IsNullOrEmpty())
            {
                string[] credentials =
                    AppSetting.Default.GoogleCredentials.Split(new string[] { "//" }, StringSplitOptions.None);

                _vertManager.SetGoogleKey(credentials[0]);
                _vertManager.SetGoogleEngineID(credentials[1]);

                _vertManager.ChangeGoogleState(AppSetting.Default.GoogleKeyVertified, true);
                _vertManager.ChangeGoogleState(AppSetting.Default.GoogleIDVertified, false);
            }
            
            if (!AppSetting.Default.EngineUsage.IsNullOrEmpty())
            {
                string[] engineBools =
                    AppSetting.Default.EngineUsage.Split(new string[] { "|" }, StringSplitOptions.None);

                cbGoogleService.IsChecked = Convert.ToBoolean(engineBools[0]);
                cbTwitterService.IsChecked = Convert.ToBoolean(engineBools[1]);
                cbYoutubeService.IsChecked = Convert.ToBoolean(engineBools[2]);
            }

            #endregion

#if DEBUG

            var mysql = new MySQLFormat<GoogleCSESearchResult>();

            mysql.Initalization("localhost", "root", "-", "plurcrawler");

            mysql.FormattingData(null);

            //var csvformat = new CSVFormat<GoogleCSESearchResult>();

            //csvformat.FormattingData(new List<GoogleCSESearchResult>() { new GoogleCSESearchResult()
            //{
            //    OriginalURL = "www.naver.com",
            //    PublishedDate = DateTime.Now,
            //    Snippet = "jasdfkjklasdfkjl;adsfs",
            //    Title = "asdf"
            //}});
#endif
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            #region [  Save Setting  ]

            // 옵션 저장

            var serializer = new ObjectSerializer<GoogleCSESearchOption>();
            AppSetting.Default.GoogleOption = serializer.Serialize(_detailsOption.GetGoogleCSESearchOption());

            AppSetting.Default.EngineUsage = $@"{cbGoogleService.IsChecked.ToString()
                                              }|{cbTwitterService.IsChecked.ToString()
                                              }|{cbYoutubeService.IsChecked.ToString()}";


            // 인증 정보 저장

            if (!_vertManager.GoogleAPIKey.IsNullOrEmpty() ||
                !_vertManager.GoogleEngineID.IsNullOrEmpty())
            {
                AppSetting.Default.GoogleCredentials = $"{_vertManager.GoogleAPIKey}//{_vertManager.GoogleEngineID}";
                AppSetting.Default.GoogleKeyVertified = _vertManager.GoogleAPIVerifyType;
                AppSetting.Default.GoogleIDVertified = _vertManager.GoogleEngineIDVerifyType;
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

        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {
            mainTabControl.SelectedIndex = 0;
            tbSelectedName.Text = (mainTabControl.SelectedItem as TabItem).Tag.ToString();
        }

        private void BtnVertManager_Click(object sender, RoutedEventArgs e)
        {
            mainTabControl.SelectedIndex = 1;
            tbSelectedName.Text = (mainTabControl.SelectedItem as TabItem).Tag.ToString();
        }
        
        private void BtnTaskReport_Click(object sender, RoutedEventArgs e)
        {
            mainTabControl.SelectedIndex = 2;
            tbSelectedName.Text = (mainTabControl.SelectedItem as TabItem).Tag.ToString();
        }

        #endregion
        
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
    }
}
