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

using Newtonsoft.Json;
using PlurCrawler;
using PlurCrawler.Search;
using PlurCrawler.Search.Services.GoogleCSE;
using PlurCrawler.Search.Services.Twitter;
using PlurCrawler.Search.Services.Youtube;
using PlurCrawler.Tokens.Credentials;
using PlurCrawler.Tokens.Tokenizer;
using PlurCrawler_Sample.Windows;

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
        }
        
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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            #region [  Initalization  ]

            _detailsOption = (DetailsOption)frOption.Content;
            _vertificationManager = (VertificationManager)frVertManager.Content;

            #endregion

            //var cred = new TwitterCredentials("hKomvFO7HT0ZNZM9Kc2lnKhsY", "mk2AoM6iHKuSPmnvsqyHJuKXqIsDVHD37hoB3KA6Y6oksNDhyD");

            //TwitterTokenizer tokenizer = new TwitterTokenizer();

            //string url = tokenizer.GetURL(cred);

            //string pin = "";

            //cred.InputPIN(pin);

            //tokenizer.CredentialsCertification(cred);

            //TwitterSearcher searcher = new TwitterSearcher();

            //searcher.Search(new TwitterSearchOption()
            //{
            //    Query = "대도서관"
            //});
        }
    }
}
