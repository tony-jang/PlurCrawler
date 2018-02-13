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
        }

        private List<TaskReportData> _datas;

        public void AddReport(TaskReportData data)
        {
            _datas.Add(data);
        }

        public void SetReport(TaskReportData data)
        {
            tbSearchDate.Text = data.SearchDate.ToString("yyyy-mm-dd tt hh:mm");
            tbSearchCount.Text = data.SearchCount.ToString();
            switch (data.SearchResult)
            {
                case SearchResult.Success:
                    tbSearchResult.Foreground = Brushes.Green;
                    tbSearchResult.Text = "성공한 요청";
                    break;
                case SearchResult.Fail_InvaildSetting:
                    tbSearchResult.Foreground = Brushes.Red;
                    tbSearchResult.Text = "실패 - 올바르지 않은 설정";
                    break;
                case SearchResult.Fail_APIError:
                    tbSearchResult.Foreground = Brushes.Red;
                    tbSearchResult.Text = "실패 - API 오류로 인한 실패";
                    break;
            }

            string[] serviceString = { "Google Custom Search Engine", "Youtube", "Twitter" };
            tbRequestService.Text = serviceString[(int)data.RequestService];


        }
    }
}
