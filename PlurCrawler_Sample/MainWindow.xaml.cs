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
using PlurCrawler.Search.Options;
using PlurCrawler.Search.SearchResults;
using PlurCrawler.Tokens.Credentials;
using PlurCrawler.Tokens.Tokenizer;

namespace PlurCrawler_Sample
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Test();
        }

        public void Test()
        {
            var naverCredentials = new NaverCredentials()
            {
                ClientId = "dd4WOSPzTtUF_uKyc9iI",
                ClientSecret = "vURlgeXkH6"
            };

            var naverTokenizer = new NaverTokenizer();
            NaverToken naverToken = (NaverToken)naverTokenizer.CredentialsCertification(naverCredentials);

            var searcher = new NaverNewsSearcher(naverToken);

            searcher.Search(new NaverSearchOption()
            {
                Query = "Google",
                SearchCount = 10
            });
            
            //NaverSearchResult naverSearchResult = new NaverSearchResult()
            //{
            //    Date = DateTime.Now,
            //    OriginalURL = "asdf",
            //    Title = "asdf"
            //};

            //NaverSearchResult naverSearchResult2 = new NaverSearchResult()
            //{
            //    Date = DateTime.Now,
            //    OriginalURL = "asdf2",
            //    Title = "asdf2"
            //};

            //List<NaverSearchResult> results = new List<NaverSearchResult>(){ naverSearchResult, naverSearchResult2 };

            //var str = JsonConvert.SerializeObject(results);

            //Clipboard.Clear();
            //Clipboard.SetText(str);

            return;
            TwitterCredentials credentials = new TwitterCredentials("hKomvFO7HT0ZNZM9Kc2lnKhsY", "mk2AoM6iHKuSPmnvsqyHJuKXqIsDVHD37hoB3KA6Y6oksNDhyD");

            string url = credentials.GetURL();
            string pin = "";

            credentials.InputPIN(pin);

            TwitterTokenizer twitterTokenizer = new TwitterTokenizer();

            twitterTokenizer.CredentialsCertification(credentials);
        }
    }
}
