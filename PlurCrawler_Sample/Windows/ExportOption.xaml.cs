using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.IO;


using PlurCrawler_Sample.Export;
using PlurCrawler_Sample.Common;

using IOPath = System.IO.Path;

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
        }

        public void SettingChanged(object sender, EventArgs e)
        {
            SettingManager.ExportOptionSetting = ExportSetting();
        }

        #region [  Setting Load / Save  ]

        public void LoadSetting(ExportOptionSetting option)
        {
            jsonExportFolder.Text = option.JsonFolderLocation ;
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
    }
}
