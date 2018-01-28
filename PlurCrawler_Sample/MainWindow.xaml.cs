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
            YoutubeSearcher searcher2 = new YoutubeSearcher();

            MessageBox.Show(searcher2.GetLanguageCode(ServiceKind.Twitter, LanguageCode.Korean));

            return;
            
            var items2 = searcher2.Search(new YoutubeSearchOption()
            {
                Query = "싸이",
                SearchCount = 10
            });


            foreach (var itm in items2)
            {
                //crawlLB.Items.Add(itm.Title + "||" + itm.Description);
            }

            return;

            GoogleCSESearcher searcher = new GoogleCSESearcher("AIzaSyDuJa0F9nJzUwTOJyPZeBqkbEPwHoxwTG4", "010840099607507232751:bvvrumuuuqw");

            var items = searcher.Search(new GoogleCSESearchOption()
            {
                Query = "앙 기모띠",
                SearchCount = 10
            });

            foreach(var itm in items)
            {
                //crawlLB.Items.Add(itm.Title + "||" + itm.Snippet);
            }

        }
    }
}
