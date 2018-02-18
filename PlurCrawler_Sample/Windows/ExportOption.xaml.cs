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
using System.Windows.Forms;
using System.IO;

using PlurCrawler.Extension;

using PlurCrawler_Sample.Export;
using PlurCrawler_Sample.Common;

using Microsoft.Win32;

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
            setJsonLocationPath.Click += SetJsonLocationPath_Click;
            setCsvLocationPath.Click += SetCsvLocationPath_Click;
        }
        
        #region [  Setting Load / Save  ]

        public void LoadSettingFromString(string optionString)
        {
            if (optionString.IsNullOrEmpty())
                return;

            var serializer = new ObjectSerializer<ExportOptionSetting>();
            var itm = serializer.Deserialize(optionString);

            jsonExportFolder.Text = itm.JsonFolderLocation ;
            jsonExportName.Text = itm.JsonFileName;
            jsonOverlapOption.SelectedIndex = itm.JsonOverlapOption;
            cbUseJsonSort.IsChecked = itm.JsonSort;

            csvExportFolder.Text = itm.CSVFolderLocation;
            csvExportName.Text = itm.CSVFileName;
            csvOverlapOption.SelectedIndex = itm.CSVOverlapOption;

            mysqlConnAddr.Text = itm.MySQLConnAddr;
            mysqlUserID.Text = itm.MySQLUserID;
            mysqlUserPw.Password = itm.MySQLUserPassword;
            mysqlDatabaseName.Text = itm.MySQLDatabaseName;

            cbMySQLManualInput.IsChecked = itm.MySQLManualInput;
            mysqlSelfConnQuery.Text = itm.MySQLConnString;
        }

        public string ExportSettingString()
        {
            var optStr = new ExportOptionSetting()
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
                MySQLConnString = mysqlSelfConnQuery.Text
            };

            var serializer = new ObjectSerializer<ExportOptionSetting>();

            return serializer.Serialize(optStr);
        }

        #endregion

        #region [  Json  ]

        public bool UseJsonSort => cbUseJsonSort.IsChecked.GetValueOrDefault();

        /// <summary>
        /// Json으로 내보낼 경로 + 파일 이름을 나타냅니다.
        /// </summary>
        public string JsonFullPath => IOPath.Combine(jsonExportFolder.Text, $"{jsonExportName.Text}.json");

        /// <summary>
        /// Json으로 내보낼 폴더를 나타냅니다.
        /// </summary>
        public string JsonFolderPath => jsonExportFolder.Text;

        /// <summary>
        /// Json으로 내보낼 파일 이름 부분만 나타냅니다.
        /// </summary>
        public string JsonFileName => jsonExportName.Text;

        private void SetJsonLocationPath_Click(object sender, RoutedEventArgs e)
        {
            jsonExportFolder.Text = GetFolderPath(jsonExportFolder.Text);
        }

        #endregion

        #region [  CSV  ]

        /// <summary>
        /// CSV로 내보낼 경로 + 파일 이름을 나타냅니다.
        /// </summary>
        public string CSVFullPath => IOPath.Combine(csvExportFolder.Text, $"{csvExportName.Text}.csv");

        /// <summary>
        /// CSV로 내보낼 폴더를 나타냅니다.
        /// </summary>
        public string CSVFolderPath => csvExportFolder.Text;

        /// <summary>
        /// CSV로 내보낼 파일 이름 부분만 나타냅니다.
        /// </summary>
        public string CSVFileName => csvExportName.Text;

        private void SetCsvLocationPath_Click(object sender, RoutedEventArgs e)
        {
            csvExportFolder.Text = GetFolderPath(csvExportFolder.Text);
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
