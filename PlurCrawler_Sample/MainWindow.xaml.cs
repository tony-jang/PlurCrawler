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
using System.Net;
using System.IO;

namespace PlurCrawler_Sample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        DetailsOption _detailsOption;
        VertificationManager _vertManager;
        ExportOption _exportOption;
        TaskReport _taskReport;

        Dictionary<ISearcher, TaskProgressBar> dict;

        bool AutoPreviewItemFocus { get; set; } = false;

        public MainWindow()
        {
            InitializeComponent();

            #region [  Initalization  ]

            dict = new Dictionary<ISearcher, TaskProgressBar>();

            TaskLogManager.Init();
            SettingManager.Init();

            Pair<bool,bool,bool> eUsage = SettingManager.EngineUsage;

            cbGoogleService.IsChecked = eUsage.Item1;
            cbTwitterService.IsChecked = eUsage.Item2;
            cbYoutubeService.IsChecked = eUsage.Item3;

            CheckVertified(ServiceKind.GoogleCSE);
            CheckVertified(ServiceKind.Twitter);
            CheckVertified(ServiceKind.Youtube);

            SettingManager.GoogleCredentials.PropertyChanged += GoogleCredentials_PropertyChanged;
            SettingManager.TwitterCredentialChanged += SettingManager_TwitterCredentialChanged;
            SettingManager.YoutubeCredentials.PropertyChanged += YoutubeCredentials_PropertyChanged;
            
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

            TaskLogManager.LogAdded += LogManager_LogAdded;

            previewTabAutoFocusing.Checked += PreviewTabAutoFocusing_Checked;
            previewTabAutoFocusing.Unchecked += PreviewTabAutoFocusing_Checked;

            this.Closing += MainWindow_Closing;

            #endregion
        }

        private void PreviewTabAutoFocusing_Checked(object sender, RoutedEventArgs e)
        {
            AutoPreviewItemFocus = previewTabAutoFocusing.IsChecked.GetValueOrDefault();
        }

        #region [  인증 상태 관리  ]

        public void CheckVertified(ServiceKind serviceKind)
        {
            switch (serviceKind)
            {
                case ServiceKind.GoogleCSE:
                    if (SettingManager.GoogleCredentials.Item2 == VerifyType.Verified &&
                        SettingManager.GoogleCredentials.Item4 == VerifyType.Verified)
                    {
                        cbGoogleVertified.IsChecked = true;
                        goVertToolTip.Content = "구글 CSE 엔진은 인증되었습니다.";
                    }
                    else
                    {
                        cbGoogleVertified.IsChecked = false;
                        goVertToolTip.Content = "구글 CSE 엔진은 인증되지 않았습니다.";
                    }
                    break;
                case ServiceKind.Youtube:
                    if (SettingManager.YoutubeCredentials.Item2 == VerifyType.Verified)
                    {
                        cbYoutubeVertified.IsChecked = true;
                        ytVertToolTip.Content = "Youtube 엔진은 인증되었습니다.";
                    }
                    else
                    {
                        cbYoutubeVertified.IsChecked = false;
                        ytVertToolTip.Content = "Youtube 엔진은 인증되지 않았습니다.";
                    }
                    break;
                case ServiceKind.Twitter:
                    if (SettingManager.TwitterVertified)
                    {
                        cbTwitterVertified.IsChecked = true;
                        twVertToolTip.Content = "Twitter 엔진은 인증되었습니다.";
                    }
                    else
                    {
                        cbTwitterVertified.IsChecked = false;
                        twVertToolTip.Content = "Twitter 엔진은 인증되지 않았습니다." ;
                    }
                    break;
                default:
                    break;
            }
        }

        private void YoutubeCredentials_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Item2":
                    CheckVertified(ServiceKind.Youtube);
                    break;
            }
        }

        private void SettingManager_TwitterCredentialChanged(object sender, EventArgs e)
        {
            CheckVertified(ServiceKind.Twitter);
        }

        private void GoogleCredentials_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Item2":
                case "Item4":
                    CheckVertified(ServiceKind.GoogleCSE);
                    break;
            }
        }

        #endregion

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

        private void LogManager_LogAdded(TaskLog taskLog)
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

            if (!AccessDBFormat<ISearchResult>.AccessConnectorInstalled())
            {
                AddLog("Access DB에 올바르게 접근 할 수 없습니다. 내보내기 옵션의 Access DB에서 문제를 해결하세요.", TaskLogType.System);
            }
            else
            {
                _exportOption.HiddenInstall();
            }

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
