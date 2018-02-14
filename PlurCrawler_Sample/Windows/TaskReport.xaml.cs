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
using PlurCrawler.Search;
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

            lvReportList.SelectionChanged += LvReportList_SelectionChanged;

            btnShowList.Click += BtnShowList_Click;
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

        public int SelectedIndex { get; internal set; }

        private void LvReportList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvReportList.SelectedIndex != -1)
                SetReport(lvReportList.SelectedIndex);
        }

        private List<TaskReportData> _datas;

        public void AddReport(TaskReportData data)
        {
            lvReportList.Items.Add(data.SearchDate.ToString("yyyy-MM-dd tt hh:mm 요청"));
            _datas.Add(data);
        }
        
        public void SetReport(int index)
        {
            SetReport(_datas[index]);
        }

        public void SetLastReport()
        {
            if (_datas.Count > 0)
                SetReport(_datas[_datas.Count - 1]);
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
            string[] serviceString = { "Google Custom Search Engine", "Youtube", "Twitter" };
            tbRequestService.Text = serviceString[(int)data.RequestService];

            SetForeground(brush);
        }

        private void SetForeground(Brush brush)
        {
            tbSearchResult.Foreground = brush;
            tbSearchDate.Foreground = brush;
            tbRequestService.Foreground = brush;
            tbSearchCount.Foreground = brush;
        }
    }
}
