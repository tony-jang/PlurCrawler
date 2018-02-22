using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

using PlurCrawler.Search.Base;
using PlurCrawler.Extension;
using PlurCrawler.Format.Common;
using PlurCrawler.Common;
using PlurCrawler.Search;
using PlurCrawler.Format;
using PlurCrawler.Search.Services.GoogleCSE;
using PlurCrawler.Search.Services.Youtube;

using PlurCrawler_Sample.Windows;
using PlurCrawler_Sample.Controls;
using PlurCrawler_Sample.Common;
using PlurCrawler_Sample.TaskLogs;

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
        ExportOption _exportOption;
        TaskReport _taskReport;

        Dictionary<ISearcher, TaskProgressBar> dict;

        public MainWindow()
        {
            InitializeComponent();

            //var format = new MySQLFormat<YoutubeSearchResult>("localhost", "root", "asdf", "plurcrawler", "ttest");

            //string str = format.GetCreateTableQuery();

            //return;

            #region [  Initalization  ]

            _logManager = new TaskLogManager();
            
            dict = new Dictionary<ISearcher, TaskProgressBar>();

            SettingManager.Init();

            Pair<bool,bool,bool> eUsage = SettingManager.EngineUsage;

            cbGoogleService.IsChecked = eUsage.Item1;
            cbTwitterService.IsChecked = eUsage.Item2;
            cbYoutubeService.IsChecked = eUsage.Item3;

            btnSearch.IsEnabled = eUsage.Item1 || eUsage.Item2 || eUsage.Item3;

            #endregion

            #region [  Event Connection  ]

            this.Loaded += MainWindow_Loaded;

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
            btnExportOption.Click += BtnExportOption_Click;

            mainTabControl.SelectionChanged += MainTabControl_SelectionChanged;

            _logManager.LogAdded += _logManager_LogAdded;

            this.Closing += MainWindow_Closing;

            #endregion
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MainTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mainTabControl.SelectedIndex != -1)
                tbSelectedName.Text = (mainTabControl.SelectedItem as TabItem).Tag.ToString();
        }

        private void _taskReport_ExportRequest(OutputFormat format, IEnumerable<ISearchResult> result, ServiceKind serviceKind)
        {
            Export(format, result, serviceKind);
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

        public void ScrollToEnd()
        {
            try
            {
                lvLog.SelectedIndex = lvLog.Items.Count - 1;
                lvLog.ScrollIntoView(lvLog.SelectedItem);
            }
            catch (Exception)
            {
            }
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region [  Initalization UI  ]

            lvLog.Items.Clear();
            lvTask.Items.Clear();
            
            _detailsOption = (DetailsOption)frOption.Content;
            _vertManager = (VertificationManager)frVertManager.Content;
            _exportOption = (ExportOption)frExportOption.Content;
            _taskReport = (TaskReport)frTaskReport.Content;
            
            _taskReport.ExportRequest += _taskReport_ExportRequest;

            #endregion

#if DEBUG

            //var mysql = new MySQLFormat<GoogleCSESearchResult>("localhost", "root", "-", "plurcrawler", "google");

            //mysql.FormattingData(null);

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
        
        #region [  UI EventHandler  ]

        private void CheckChanged(object sender, RoutedEventArgs e)
        {
            if (cbGoogleService.IsChecked.GetValueOrDefault() ||
                cbTwitterService.IsChecked.GetValueOrDefault() ||
                cbYoutubeService.IsChecked.GetValueOrDefault())
                btnSearch.IsEnabled = true;
            else
                btnSearch.IsEnabled = false;

            SettingManager.EngineUsage.Item1 = cbGoogleService.IsChecked.GetValueOrDefault();
            SettingManager.EngineUsage.Item2 = cbTwitterService.IsChecked.GetValueOrDefault();
            SettingManager.EngineUsage.Item3 = cbYoutubeService.IsChecked.GetValueOrDefault();
        }

        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {
            mainTabControl.SelectedIndex = 0;
        }

        private void BtnVertManager_Click(object sender, RoutedEventArgs e)
        {
            mainTabControl.SelectedIndex = 1;
        }
        
        private void BtnTaskReport_Click(object sender, RoutedEventArgs e)
        {
            mainTabControl.SelectedIndex = 2;
        }

        private void BtnExportOption_Click(object sender, RoutedEventArgs e)
        {
            mainTabControl.SelectedIndex = 3;
        }
        
        #endregion

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            if (tbQuery.Text.IsNullOrEmpty())
            {
                AddLog("검색어가 없습니다.", TaskLogType.Failed);
                return;
            }

            if (cbGoogleService.IsChecked.GetValueOrDefault())
                SearchGoogle();

            if (cbTwitterService.IsChecked.GetValueOrDefault())
                TwitterSearch();

            if (cbYoutubeService.IsChecked.GetValueOrDefault())
                YoutubeSearch();
        }
    }
}
