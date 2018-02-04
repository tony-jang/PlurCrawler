using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
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

using PlurCrawler;
using PlurCrawler.Search;
using PlurCrawler.Search.Services.GoogleCSE;
using PlurCrawler.Search.Services.Twitter;
using PlurCrawler.Search.Services.Youtube;
using PlurCrawler.Tokens.Credentials;
using PlurCrawler.Tokens.Tokenizer;
using PlurCrawler_Sample.Windows;

using Newtonsoft.Json;

namespace PlurCrawler_Sample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        DetailsOption _detailsOption;
        VertificationManager _vertificationManager;

        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;

            btnSearch.Click += BtnSearch_Click;

            itm.Maximum = 100;

            Thread thr = new Thread(() =>
            {
                int i = 0;
                while (i <= 100)
                {
                    Dispatcher.Invoke(() => itm.Value = i++);
                    Thread.Sleep(20);
                }

                Dispatcher.Invoke(() =>
                {
                    _detailsOption.signGoogle.Visibility = Visibility.Hidden;
                });
            });

            thr.Start();
        }

        bool en = false;

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            var googleCSESearcher = new GoogleCSESearcher();

            googleCSESearcher.Vertification(_vertificationManager.GoogleAPIKey, _vertificationManager.GoogleEngineID);

            mainTabControl.SelectedIndex = 0;

            lvLog.Items.Add("[System] 검색을 시작합니다.");

            var list = googleCSESearcher.Search(new GoogleCSESearchOption()
            {
                SearchCount = 10,
                Query = "네이버"
            });

            lvLog.Items.Add("[System] 검색이 완료되었습니다.");

            foreach (var itm in list)
            {
                lvLog.Items.Add(new ListViewItem()
                {
                    Content = itm.Title
                });
            }
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region [  Initalization  ]

            _detailsOption = (DetailsOption)frOption.Content;
            _vertificationManager = (VertificationManager)frVertManager.Content;

            #endregion
            
#if DEBUG
            _detailsOption.LoadGoogle(new GoogleCSESearchOption()
            {
                DateRange = new PlurCrawler.Common.DateRange(DateTime.Today, DateTime.Today),
                Offset = 3,
                SearchCount = 15,
                SplitWithDate = false
            });
            
            var setting = _detailsOption.GetGoogleCSESearchOption();

            Console.WriteLine("test");

            //foreach (PropertyInfo mi in typeof(GoogleCSESearchResult).GetProperties())
            //{
            //Console.Write("test");
            //string s = (string)GetPropValue(new GoogleCSESearchResult()
            //{
            //    OriginalURL = "http://naver.com"
            //}, "OriginalURL");

            //MessageBox.Show(s);
            //}
#endif
        }
    }
}
