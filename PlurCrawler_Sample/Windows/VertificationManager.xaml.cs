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

using PlurCrawler.Tokens.Credentials;
using PlurCrawler.Tokens.Tokenizer;
using PlurCrawler_Sample.Common;

namespace PlurCrawler_Sample.Windows
{
    /// <summary>
    /// VertificationManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class VertificationManager : Page
    {
        const string HiddenText = "******************";

        public VertificationManager()
        {
            InitializeComponent();

            btnGoogleOK.Click += BtnGoogleOK_Click;
            btnGoogleViewHidden.Click += BtnGoogleViewHidden_Click;
            btnTwitterPINAuth.Click += BtnTwitterPINAuth_Click;

            btnTwitterReqURL.Click += BtnTwitterReqURL_Click;
        }

        #region [  Twitter  ]

        TwitterTokenizer tokenizer;
        TwitterCredentials credentials;

        private void BtnTwitterPINAuth_Click(object sender, RoutedEventArgs e)
        {
            tcTwitterAuth.SelectedIndex = 2;
            string pinNumber = tbTwitterPIN.Text;

            credentials.InputPIN(pinNumber);
            tokenizer.CredentialsCertification(credentials);

            wbTwitter.Visibility = Visibility.Hidden;
        }

        private void BtnTwitterReqURL_Click(object sender, RoutedEventArgs e)
        {
            string errMsg1 = "잘못된 Consumer Key 또는 Consumer Secret이 입력되었습니다.";
            string errMsg2 = "Consumer Key또는 Consumer Secret은 빈칸일 수 없습니다.";

            if (string.IsNullOrEmpty(tbTwitterKey.Text) || string.IsNullOrEmpty(tbTwitterSecret.Password))
            {
                tbTwitterMsg.Visibility = Visibility.Visible;
                tbTwitterMsg.Text = errMsg2;

                return;
            }

            credentials = new TwitterCredentials(tbTwitterKey.Text, tbTwitterSecret.Password);
            tokenizer = new TwitterTokenizer();

            string url = tokenizer.GetURL(credentials);

            if (!string.IsNullOrEmpty(url))
            {
                tbTwitterMsg.Visibility = Visibility.Hidden;

                runTwitterID.Text = tbTwitterKey.Text;
                runTwitterSecret.Text = runTwitterSecret.Text; 

                tcTwitterAuth.SelectedIndex = 1;
                wbTwitter.Navigate(url);
            }
            else
            {
                tbTwitterMsg.Text = errMsg1;
                tbTwitterMsg.Visibility = Visibility.Visible;
            }
        }

        #endregion

        #region [  Google CSE  ]

        private bool IsGoogleHidden { get; set; }

        /// <summary>
        /// 구글의 API 키를 나타냅니다.
        /// </summary>
        public string GoogleAPIKey { get; private set; }

        /// <summary>
        /// 구글의 엔진 ID를 나타냅니다.
        /// </summary>
        public string GoogleEngineID { get; private set; }

        public void SetGoogleKey(string key, string googleEngineId)
        {
            tbGoogleKey.Clear();

            GoogleAPIKey = key;
            GoogleEngineID = googleEngineId;

            IsGoogleHidden = true;

            runGoogleAPIKey.Text = HiddenText;
            ChangeGoogleState(VerifyType.Invalid);
        }

        public void ChangeGoogleState(VerifyType verifyType)
        {
            Brush brush;
            string text;
            switch (verifyType)
            {
                case VerifyType.Verified:
                    brush = Brushes.Green;
                    text = "인증됨";
                    break;
                case VerifyType.NotChecked:
                    brush = Brushes.Orange;
                    text = "확인되지 않음";
                    break;
                case VerifyType.Invalid:
                default:
                    brush = Brushes.Red;
                    text = "유효하지 않음";
                    break;
            }
            runGoogleAPIKey.Foreground = brush;
            runGoogleAvailable.Foreground = brush;
            runEngineID.Foreground = brush;

            runGoogleAvailable.Text = text;
        }

        private void BtnGoogleOK_Click(object sender, RoutedEventArgs e)
        {
            SetGoogleKey(tbGoogleKey.Text, tbEngineID.Text);
        }

        private void BtnGoogleViewHidden_Click(object sender, RoutedEventArgs e)
        {
            if (IsGoogleHidden)
                runGoogleAPIKey.Text = GoogleAPIKey;
            else
                runGoogleAPIKey.Text = HiddenText;

            IsGoogleHidden = !IsGoogleHidden;
        }


        #endregion

        private void Button_Click()
        {

        }
    }
}
