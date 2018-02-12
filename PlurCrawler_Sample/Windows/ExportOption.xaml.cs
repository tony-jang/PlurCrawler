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
            setLocationPath.Click += SetLocationPath_Click;
        }

        private void SetLocationPath_Click(object sender, RoutedEventArgs e)
        {
            jsonExportFolder.Text = GetFolderPath(jsonExportFolder.Text);
        }

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
