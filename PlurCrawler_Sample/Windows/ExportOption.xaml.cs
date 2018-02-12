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

using Microsoft.Win32;
using System.IO;
using PlurCrawler_Sample.Export;
using PlurCrawler_Sample.Common;

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
        }

        #region [  Json  ]

        public void LoadSettingFromString(string optionString)
        {
            if (string.IsNullOrEmpty(optionString))
                return;

            var serializer = new ObjectSerializer<ExportOptionStruct>();
            var itm = serializer.Deserialize(optionString);

            jsonExportFolder.Text = itm.JsonFolderLocation ;
            jsonExportName.Text = itm.JsonFileName;
            cbOverlapOption.SelectedIndex = itm.JsonOverlapOption;
            cbUseJsonSort.IsChecked = itm.JsonSort;
            csvExportFolder.Text = itm.CSVFolderLocation;
            csvExportName.Text = itm.CSVFileName;
            csvOverlapOption.SelectedIndex = itm.CSVOverlapOption;
            mysqlConnAddr.Text = itm.MySQLConnAddr;
            mysqlUserID.Text = itm.MySQLUserID;
            mysqlUserPw.Password = itm.MySQLUserPassword;
            mysqlDatabaseName.Text = itm.MySQLDatabaseName;
            mysqlSelfConnQuery.Text = itm.MySQLConnString;
        }

        public string ExportSettingString()
        {
            var optStr = new ExportOptionStruct()
            {
                JsonFolderLocation = jsonExportFolder.Text,
                JsonFileName = jsonExportName.Text,
                JsonOverlapOption = cbOverlapOption.SelectedIndex,
                JsonSort = cbUseJsonSort.IsChecked.GetValueOrDefault(),
                CSVFolderLocation = csvExportFolder.Text,
                CSVFileName = csvExportName.Text,
                CSVOverlapOption = csvOverlapOption.SelectedIndex,
                MySQLConnAddr = mysqlConnAddr.Text,
                MySQLUserID = mysqlUserID.Text,
                MySQLUserPassword = mysqlUserPw.Password,
                MySQLDatabaseName = mysqlDatabaseName.Text,
                MySQLConnString = mysqlSelfConnQuery.Text
            };

            var serializer = new ObjectSerializer<ExportOptionStruct>();

            return serializer.Serialize(optStr);
        }

        private void SetJsonLocationPath_Click(object sender, RoutedEventArgs e)
        {
            jsonExportFolder.Text = GetFolderPath(jsonExportFolder.Text);
        }

        #endregion



        public string GetFolderPath(string startPath)
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
