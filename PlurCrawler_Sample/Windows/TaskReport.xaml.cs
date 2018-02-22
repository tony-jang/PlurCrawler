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

using PlurCrawler.Attributes;
using PlurCrawler.Extension;
using PlurCrawler.Format.Common;
using PlurCrawler.Search;
using PlurCrawler.Search.Base;

using PlurCrawler_Sample.Report;

namespace PlurCrawler_Sample.Windows
{
    /// <summary>
    /// TaskReport.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TaskReport : Page
    {
        public TaskReport()
        {
            InitializeComponent();
            _datas = new List<TaskReportData>();

            #region [  Add EventHandler  ]

            lvReportList.SelectionChanged += LvReportList_SelectionChanged;

            btnShowList.Click += BtnShowList_Click;

            btnJsonExport.Click += BtnJsonExport_Click;
            btnCSVExport.Click += BtnCSVExport_Click;
            btnMySQLExport.Click += BtnMySQLExport_Click;
            btnAccessDBExport.Click += BtnAccessDBExport_Click;

            #endregion

            ChangeButtonEnabled(false);
        }

        public void ChangeButtonEnabled(bool isEnabled)
        {
            btnJsonExport.IsEnabled = isEnabled;
            btnCSVExport.IsEnabled = isEnabled;
            btnMySQLExport.IsEnabled = isEnabled;
            btnAccessDBExport.IsEnabled = isEnabled;
        }

        public delegate void ExportRequestDelegate(OutputFormat format, IEnumerable<ISearchResult> result, ServiceKind serviceKind);

        public event ExportRequestDelegate ExportRequest;

        private void OnExportRequest(OutputFormat format, IEnumerable<ISearchResult> result, ServiceKind serviceKind)
        {
            ExportRequest?.Invoke(format, result, serviceKind);
        }

        private void BtnAccessDBExport_Click(object sender, RoutedEventArgs e)
        {
            OnExportRequest(OutputFormat.AccessDB, SelectionResult, ServiceKind);
        }

        private void BtnMySQLExport_Click(object sender, RoutedEventArgs e)
        {
            OnExportRequest(OutputFormat.MySQL, SelectionResult, ServiceKind);
        }

        private void BtnCSVExport_Click(object sender, RoutedEventArgs e)
        {
            OnExportRequest(OutputFormat.CSV, SelectionResult, ServiceKind);
        }

        private void BtnJsonExport_Click(object sender, RoutedEventArgs e)
        {
            OnExportRequest(OutputFormat.Json, SelectionResult, ServiceKind);
        }

        private void BtnShowList_Click(object sender, RoutedEventArgs e)
        {
            if (!(bool)btnShowList.Tag)
            {
                btnShowList.Content = "> 목록 보이기";
                colDefinition.Width = new GridLength(0);
            }
            else
            {
                btnShowList.Content = "< 목록 숨기기";
                colDefinition.Width = new GridLength(250);
            }

            btnShowList.Tag = !(bool)btnShowList.Tag;
        }

        private void LvReportList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvReportList.SelectedIndex != -1)
                SetReport(lvReportList.SelectedIndex);
        }

        public int SelectedIndex { get; private set; } = -1;

        private IEnumerable<ISearchResult> SelectionResult => _datas[SelectedIndex].SearchData;

        private ServiceKind ServiceKind => _datas[SelectedIndex].RequestService;

        private List<TaskReportData> _datas;

        public void AddReport(TaskReportData data)
        {
            lvReportList.Items.Add(data.SearchDate.ToString("yyyy-MM-dd tt hh:mm 요청"));
            _datas.Add(data);
        }
        
        public void SetReport(int index)
        {
            SetReport(_datas[index]);

            SelectedIndex = index;
        }

        public void SetLastReport()
        {
            if (_datas.Count > 0)
            {
                SetReport(_datas[_datas.Count - 1]);
                SelectedIndex = _datas.Count - 1;
            }
        }

        private void SetReport(TaskReportData data)
        {
            Brush brush = null;

            tbSearchQuery.Text = data.Query;
            tbSearchDate.Text = data.SearchDate.ToString("yyyy-MM-dd tt hh:mm");
            tbSearchCount.Text = data.SearchCount.ToString();
            switch (data.SearchResult)
            {
                case SearchResult.Success:
                    brush = Brushes.Green;
                    tbSearchResult.Text = "성공한 요청";
                    break;
                case SearchResult.Fail_InvaildSetting:
                    brush = Brushes.Red;
                    tbSearchResult.Text = "실패 - 올바르지 않은 설정";
                    break;
                case SearchResult.Fail_APIError:
                    brush = Brushes.Red;
                    tbSearchResult.Text = "실패 - API 오류로 인한 실패";
                    break;
            }

            ChangeButtonEnabled(data.SearchResult == SearchResult.Success);

            try
            {
                TextBlock[] tbList = { tbJsonInfo, tbCSVInfo, tbMySQLInfo, tbAccessDBInfo };
                Enum[] resultEnumList = { data.ExportResultPack.JsonExportResult,
                                      data.ExportResultPack.CSVExportResult,
                                      data.ExportResultPack.MySQLExportResult,
                                      data.ExportResultPack.AccessExportResult};

                Enum[] serviceEnumList = { OutputFormat.Json, OutputFormat.CSV, OutputFormat.MySQL, OutputFormat.AccessDB };

                for (int i = 0; i <= 3; i++)
                {
                    tbList[i].Text = resultEnumList[i].GetAttributeFromEnum<NoteAttribute>().Message;

                    if (data.OutputFormat.HasFlag(serviceEnumList[i]))
                    {
                        tbList[i].Foreground = (resultEnumList[i].GetAttributeFromEnum<BoolAttribute>().Value ? Brushes.Green : Brushes.Red);
                    }
                    else
                    {
                        tbList[i].Foreground = Brushes.Black;
                    }
                }
            }
            catch (Exception)
            {
            }
            
            
            string[] serviceString = { "Google Custom Search Engine", "Youtube", "Twitter" };
            tbRequestService.Text = serviceString[(int)data.RequestService];

            SetForeground(brush);
        }

        private void SetForeground(Brush brush)
        {
            tbSearchQuery.Foreground = brush;
            tbSearchResult.Foreground = brush;
            tbSearchDate.Foreground = brush;
            tbRequestService.Foreground = brush;
            tbSearchCount.Foreground = brush;
        }
    }
}
