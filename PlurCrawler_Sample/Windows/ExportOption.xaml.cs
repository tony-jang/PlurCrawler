using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Threading;
using System.Diagnostics;

using PlurCrawler.Format;
using PlurCrawler.Search.Base;

using PlurCrawler_Sample.Export;
using PlurCrawler_Sample.Common;

using IOPath = System.IO.Path;
using WinApplication = System.Windows.Application;

namespace PlurCrawler_Sample.Windows
{
    /// <summary>
    /// ExportOption.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ExportOption : Page
    {
        public ExportOption()
        {
            InitializeComponent();

            LoadSetting(SettingManager.ExportOptionSetting);

            setJsonLocationPath.Click += SetJsonLocationPath_Click;
            setCsvLocationPath.Click += SetCsvLocationPath_Click;
            setAccLocationPath.Click += SetAccLocationPath_Click;

            jsonExportFolder.TextChanged += SettingChanged;
            jsonExportName.TextChanged += SettingChanged;
            jsonOverlapOption.SelectionChanged += SettingChanged;
            cbUseJsonSort.Checked += SettingChanged;
            cbUseJsonSort.Unchecked += SettingChanged;

            csvExportFolder.TextChanged += SettingChanged;
            csvExportName.TextChanged += SettingChanged;
            csvOverlapOption.SelectionChanged += SettingChanged;

            mysqlConnAddr.TextChanged += SettingChanged;
            mysqlUserID.TextChanged += SettingChanged;
            mysqlUserPw.PasswordChanged += SettingChanged;
            mysqlDatabaseName.TextChanged += SettingChanged;

            accExportFolder.TextChanged += SettingChanged;
            accExportName.TextChanged += SettingChanged;

            cbMySQLManualInput.Checked += SettingChanged;
            mysqlSelfConnQuery.TextChanged += SettingChanged;

            btnInstallFile.Click += BtnInstallFile_Click;
        }

        private void BtnInstallFile_Click(object sender, RoutedEventArgs e)
        {
            tbInstallLoad.Visibility = Visibility.Visible;
            btnInstallFile.IsEnabled = false;
            btnInstallFile.Content = "설치중";
            DownloadDBProvider();
        }

        public void SettingChanged(object sender, EventArgs e)
        {
            SettingManager.ExportOptionSetting = ExportSetting();
        }

        private string GetFolderPath(string startPath)
        {
            var folderDialog = new FolderBrowserDialog();

            if (Directory.Exists(startPath))
                folderDialog.SelectedPath = startPath;

            if (folderDialog.ShowDialog() == DialogResult.OK)
            {
                return folderDialog.SelectedPath;
            }

            return startPath;
        }

        #region [  Setting Load / Save  ]

        public void LoadSetting(ExportOptionSetting option)
        {
            jsonExportFolder.Text = option.JsonFolderLocation;
            jsonExportName.Text = option.JsonFileName;
            jsonOverlapOption.SelectedIndex = option.JsonOverlapOption;
            cbUseJsonSort.IsChecked = option.JsonSort;

            csvExportFolder.Text = option.CSVFolderLocation;
            csvExportName.Text = option.CSVFileName;
            csvOverlapOption.SelectedIndex = option.CSVOverlapOption;

            mysqlConnAddr.Text = option.MySQLConnAddr;
            mysqlUserID.Text = option.MySQLUserID;
            mysqlUserPw.Password = option.MySQLUserPassword;
            mysqlDatabaseName.Text = option.MySQLDatabaseName;

            accExportFolder.Text = option.AccessFolderLocation;
            accExportName.Text = option.AccessFileName;

            cbMySQLManualInput.IsChecked = option.MySQLManualInput;
            mysqlSelfConnQuery.Text = option.MySQLConnString;
        }

        public ExportOptionSetting ExportSetting()
        {
            var option = new ExportOptionSetting()
            {
                JsonFolderLocation = jsonExportFolder.Text,
                JsonFileName = jsonExportName.Text,
                JsonOverlapOption = jsonOverlapOption.SelectedIndex,
                JsonSort = cbUseJsonSort.IsChecked.GetValueOrDefault(),
                CSVFolderLocation = csvExportFolder.Text,
                CSVFileName = csvExportName.Text,
                CSVOverlapOption = csvOverlapOption.SelectedIndex,
                MySQLConnAddr = mysqlConnAddr.Text,
                MySQLUserID = mysqlUserID.Text,
                MySQLUserPassword = mysqlUserPw.Password,
                MySQLDatabaseName = mysqlDatabaseName.Text,
                MySQLManualInput = cbMySQLManualInput.IsChecked.GetValueOrDefault(),
                MySQLConnString = mysqlSelfConnQuery.Text,
                AccessFolderLocation = accExportFolder.Text,
                AccessFileName = accExportName.Text
            };

            return option;
        }

        #endregion

        #region [  Json  ]

        public bool UseJsonSort => cbUseJsonSort.IsChecked.GetValueOrDefault();

        private void SetJsonLocationPath_Click(object sender, RoutedEventArgs e)
        {
            jsonExportFolder.Text = GetFolderPath(jsonExportFolder.Text);
        }

        #endregion

        #region [  CSV  ]

        private void SetCsvLocationPath_Click(object sender, RoutedEventArgs e)
        {
            csvExportFolder.Text = GetFolderPath(csvExportFolder.Text);
        }

        #endregion

        #region [  MySQL  ]

        private void SetAccLocationPath_Click(object sender, RoutedEventArgs e)
        {
            accExportFolder.Text = GetFolderPath(accExportFolder.Text);
        }

        #endregion

        public string FullPath = IOPath.Combine(IOPath.GetTempPath(), "AccessDatabaseEngine.exe");

        public void DownloadDBProvider()
        {
            Thread thr = new Thread(() =>
            {
                try
                {
                    WebClient wc = new WebClient();

                    wc.DownloadProgressChanged += Wc_DownloadProgressChanged;
                    wc.DownloadFileCompleted += Wc_DownloadFileCompleted;
                    wc.DownloadFileAsync(new Uri("https://download.microsoft.com/download/f/d/8/fd8c20d8-e38a-48b6-8691-542403b91da1/AccessDatabaseEngine.exe"), FullPath);
                }
                catch (Exception ex)
                {
                    // TODO: 인터넷 오류 발생시 알림 추가
                }
            });

            thr.Start();
        }

        private void Wc_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                runDownloadPercent.Text = string.Empty;
                runBaseText.Text = "설치중입니다.";
                Thread thr = new Thread(() =>
                {
                    Process p = Process.Start(FullPath);

                    while (!p.HasExited)
                    {
                    }

                    if (!AccessDBFormat<ISearchResult>.AccessConnectorInstalled())
                    {
                        Dispatcher.Invoke(() =>
                        {
                            runBaseText.Text = "설치가 제대로 완료되지 않았습니다. 설치 버튼으로 재시도해보세요.";
                        });
                    }
                    else
                    {
                        Dispatcher.Invoke(() =>
                        {
                            runBaseText.Text = "설치가 완료되었습니다. 이제 5초뒤에 자동으로 재시작합니다.";
                        });
                        Thread.Sleep(5000);

                        Process.Start(WinApplication.ResourceAssembly.Location);
                        Dispatcher.Invoke(() => WinApplication.Current.Shutdown());
                    }
                });

                thr.Start();
            });
        }

        private void Wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                runDownloadPercent.Text = $"({e.ProgressPercentage}%)";
            });
        }
    }
}
