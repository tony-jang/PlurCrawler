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

using PlurCrawler_Sample.Common;

using PlurCrawler.Extension;
using PlurCrawler.Search;
using PlurCrawler.Tokens.Credentials;
using PlurCrawler.Tokens.Tokenizer;
using System.ComponentModel;

namespace PlurCrawler_Sample.Windows
{
    /// <summary>
    /// VertificationManager.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class VertificationManager : Page
    {
        const string _hiddenText = "******************";

        public VertificationManager()
        {
            InitializeComponent();

            btnGoogleKeyOK.Click += BtnGoogleKeyOK_Click;
            btnGoogleViewHidden.Click += BtnGoogleViewHidden_Click;
            btnGoogleIdOK.Click += BtnGoogleIdOK_Click;

            btnTwitterPINAuth.Click += BtnTwitterPINAuth_Click;
            btnTwitterReqURL.Click += BtnTwitterReqURL_Click;
            btnTwitterViewHidden.Click += BtnTwitterViewHidden_Click;
            btnTwitterNewAuth.Click += BtnTwitterNewAuth_Click;

            btnYTOK.Click += BtnYTOK_Click;
            btnYTViewHidden.Click += BtnYTViewHidden_Click;

            signGoogle.Visibility = Visibility.Hidden;
            signTwitter.Visibility = Visibility.Hidden;

            tbTwitterKey.TextChanged += TbTwitterKey_TextChanged;
            tbTwitterSecret.PasswordChanged += TbTwitterSecret_PasswordChanged;

            wbTwitter.Navigating += WbTwitter_Navigating;

            SettingManager.GoogleCredentials.PropertyChanged += GoogleCredentials_PropertyChanged;
            SettingManager.YoutubeCredentials.PropertyChanged += YoutubeCredentials_PropertyChanged;

            this.Loaded += VertificationManager_Loaded;
        }

        private void WbTwitter_Navigating(object sender, NavigatingCancelEventArgs e)
        {
            string currURL = e.Uri.OriginalString;
            if (!currURL.Contains("twitter.com"))
            {
                tbTwitterPINMsg.Text = "PIN 번호 입력이 취소되었습니다.";
                tbTwitterPINMsg.Visibility = Visibility.Visible;
                string url = GetTwitterURL();
                wbTwitter.Navigate(url);
            }
        }

        private void YoutubeCredentials_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Item2")
                ChangeYoutubeState(SettingManager.YoutubeCredentials.Item2);
        }

        private void GoogleCredentials_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Item2":
                    ChangeGoogleState(SettingManager.GoogleCredentials.Item2, true);
                    break;
                case "Item4":
                    ChangeGoogleState(SettingManager.GoogleCredentials.Item4, false);
                    break;
            }
        }

        private void TbTwitterSecret_PasswordChanged(object sender, EventArgs e)
        {
            SettingManager.TwitterCredentials.Item2 = tbTwitterSecret.Password;
        }

        private void TbTwitterKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            SettingManager.TwitterCredentials.Item1 = tbTwitterKey.Text;
        }

        private void VertificationManager_Loaded(object sender, RoutedEventArgs e)
        {
            if (!SettingManager.GoogleCredentials.Item1.IsNullOrEmpty())
            {
                SetGoogleKey(SettingManager.GoogleCredentials.Item1, true);
                ChangeGoogleState(SettingManager.GoogleCredentials.Item2, true);
            }
            if (!SettingManager.GoogleCredentials.Item3.IsNullOrEmpty())
            {
                SetGoogleEngineID(SettingManager.GoogleCredentials.Item3, true);
                ChangeGoogleState(SettingManager.GoogleCredentials.Item4, false);
            }

            SetTwitterAuthPair(SettingManager.TwitterCredentials.Item1, SettingManager.TwitterCredentials.Item2);

            if (!SettingManager.YoutubeCredentials.Item1.IsNullOrEmpty())
            {
                SetYoutubeKey(SettingManager.YoutubeCredentials.Item1, true);
                ChangeYoutubeState(SettingManager.YoutubeCredentials.Item2);
            }

        }

        private void ChangeYoutubeState(VerifyType verifyType)
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
                    brush = Brushes.DarkOrange;
                    text = "확인되지 않음";
                    break;
                case VerifyType.Invalid:
                default:
                    brush = Brushes.Red;
                    text = "유효하지 않음";
                    break;
            }
            
            tbYoutubeAPIKey.Foreground = brush;
            runYoutubeAPIKey.Text = _hiddenText;
            runYoutubeAPIVert.Text = text;
            IsYoutubeEncrypt = true;

            YoutubeVerifyType = verifyType;
        }

        #region [  Twitter  ]

        public void SetTwitterAuthPair(string key, string secret)
        {
            tbTwitterKey.Text = key;
            tbTwitterSecret.Password = secret;
        }

        public string TwitterKey => tbTwitterKey.Text;

        public string TwitterSecret => tbTwitterSecret.Password;

        private bool IsTwitterEncrypt { get; set; } = true;

        TwitterTokenizer tokenizer;
        TwitterCredentials credentials;
        
        private void BtnTwitterPINAuth_Click(object sender, RoutedEventArgs e)
        {
            string errMsg1 = "PIN 번호가 잘못 입력되었습니다.";
            string errMsg2 = "PIN 번호는 비어있을 수 없습니다.";

            if (tbTwitterPIN.Text.IsNullOrEmpty())
            {
                tbTwitterPINMsg.Text = errMsg2;
                tbTwitterPINMsg.Visibility = Visibility.Visible;
                return;
            }

            
            string pinNumber = tbTwitterPIN.Text;

            credentials.InputPIN(pinNumber);
            try
            {
                tokenizer.CredentialsCertification(credentials);
                SettingManager.RaiseTwitterCredentialChanged();
                tbTwitterPINMsg.Visibility = Visibility.Hidden;
                wbTwitter.Visibility = Visibility.Hidden;
                tcTwitterAuth.SelectedIndex = 2;
                tbTwitterPIN.Clear();

                runTwitterKey.Text = tbTwitterKey.Text;
                runTwitterSecret.Text = _hiddenText;
            }
            catch (CredentialsTypeException)
            {
                tbTwitterPINMsg.Text = errMsg1;
                tbTwitterPINMsg.Visibility = Visibility.Visible;
                string url = GetTwitterURL();
                wbTwitter.Navigate(url);
            }
        }

        private void BtnTwitterReqURL_Click(object sender, RoutedEventArgs e)
        {
            string errMsg1 = "잘못된 Consumer Key 또는 Consumer Secret이 입력되었습니다.";
            string errMsg2 = "Consumer Key또는 Consumer Secret은 빈칸일 수 없습니다.";

            if (tbTwitterKey.Text.IsNullOrEmpty() || tbTwitterSecret.Password.IsNullOrEmpty())
            {
                tbTwitterMsg.Visibility = Visibility.Visible;
                tbTwitterMsg.Text = errMsg2;

                return;
            }
            
            string url = GetTwitterURL();

            if (!url.IsNullOrEmpty())
            {
                tbTwitterMsg.Visibility = Visibility.Hidden;
                wbTwitter.Visibility = Visibility.Visible;
                tcTwitterAuth.SelectedIndex = 1;
                wbTwitter.Navigate(url);
            }
            else
            {
                tbTwitterMsg.Text = errMsg1;
                tbTwitterMsg.Visibility = Visibility.Visible;
            }
        }

        private string GetTwitterURL()
        {
            credentials = new TwitterCredentials(tbTwitterKey.Text, tbTwitterSecret.Password);
            tokenizer = new TwitterTokenizer();

            return tokenizer.GetURL(credentials);
        }

        private void BtnTwitterViewHidden_Click(object sender, RoutedEventArgs e)
        {
            if (IsTwitterEncrypt)
                runTwitterSecret.Text = TwitterSecret;
            else
                runTwitterSecret.Text = _hiddenText;

            IsTwitterEncrypt = !IsTwitterEncrypt;
        }
        
        private void BtnTwitterNewAuth_Click(object sender, RoutedEventArgs e)
        {
            tcTwitterAuth.SelectedIndex = 0;
            IsTwitterEncrypt = true;
        }

        #endregion

        #region [  Google CSE  ]

        public VerifyType GoogleAPIVerifyType { get; internal set; }

        public VerifyType GoogleEngineIDVerifyType { get; internal set; }

        /// <summary>
        /// 구글의 키 상태가 암호화되어 있는 상태인지를 나타냅니다.
        /// </summary>
        private bool IsGoogleEncrypt { get; set; } = true;

        /// <summary>
        /// 구글의 API 키를 나타냅니다.
        /// </summary>
        public string GoogleAPIKey { get; private set; }

        /// <summary>
        /// 구글의 엔진 ID를 나타냅니다.
        /// </summary>
        public string GoogleEngineID { get; private set; }

        /// <summary>
        /// 구글 API 키를 세팅합니다.
        /// </summary>
        /// <param name="key">Google API Key입니다.</param>
        public void SetGoogleKey(string key)
        {
            SetGoogleKey(key, false);
        }

        private void SetGoogleKey(string key, bool systemInput)
        {
            if (key.IsNullOrEmpty())
            {
                tbGoogleMsg.Visibility = Visibility.Visible;
                tbGoogleMsg.Text = "API Key는 빈칸일 수 없습니다.";
                return;
            }

            tbGoogleMsg.Visibility = Visibility.Hidden;

            btnGoogleViewHidden.IsEnabled = true;

            GoogleAPIKey = key;
            runGoogleAPIKey.Text = _hiddenText;
            IsGoogleEncrypt = true;

            if (!systemInput)
            {
                SettingManager.GoogleCredentials.Item1 = key;
                SettingManager.GoogleCredentials.Item2 = VerifyType.NotChecked;
            }
        }

        /// <summary>
        /// 구글 Engine ID를 세팅합니다.
        /// </summary>
        /// <param name="key">Google API Key입니다.</param>
        public void SetGoogleEngineID(string id)
        {
            SetGoogleEngineID(id, false);
        }

        public void SetGoogleEngineID(string id, bool systemInput)
        {
            if (id.IsNullOrEmpty())
            {
                tbGoogleMsg.Visibility = Visibility.Visible;
                tbGoogleMsg.Text = "Engine ID는 빈칸일 수 없습니다.";
                return;
            }

            tbGoogleMsg.Visibility = Visibility.Hidden;
            GoogleEngineID = id;

            if (!systemInput)
            {
                SettingManager.GoogleCredentials.Item3 = id;
                SettingManager.GoogleCredentials.Item4 = VerifyType.NotChecked;
            }
        }

        public void ChangeGoogleState(VerifyType verifyType, bool isAPIKey)
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
                    brush = Brushes.DarkOrange;
                    text = "확인되지 않음";
                    break;
                case VerifyType.Invalid:
                default:
                    brush = Brushes.Red;
                    text = "유효하지 않음";
                    break;
            }
            
            if (isAPIKey)
            {
                // API Key
                tbGoogleAPIKey.Foreground = brush;
                runGoogleAPIKey.Text = _hiddenText;
                runGoogleAPIVert.Text = text;
                IsGoogleEncrypt = true;

                GoogleAPIVerifyType = verifyType;
            }
            else
            {
                // Engine ID
                tbGoogleEngineID.Foreground = brush;
                runGoogleEngineID.Text = GoogleEngineID;
                runGoogleEngineIDVert.Text = text;

                GoogleEngineIDVerifyType = verifyType;
            }
        }

        public void ChangeEditable(bool enabled, ServiceKind kind)
        {
            switch (kind)
            {
                case ServiceKind.GoogleCSE:
                    signGoogle.Visibility = (enabled) ? Visibility.Hidden : Visibility.Visible;
                    tbGoogleInfo.IsEnabled = enabled;
                    btnGoogleKeyOK.IsEnabled = enabled;
                    btnGoogleIdOK.IsEnabled = enabled;
                    break;
                case ServiceKind.Twitter:
                    signTwitter.Visibility = (enabled) ? Visibility.Hidden : Visibility.Visible;
                    btnTwitterNewAuth.IsEnabled = enabled;
                    btnTwitterPINAuth.IsEnabled = enabled;
                    btnTwitterReqURL.IsEnabled = enabled;
                    btnTwitterViewHidden.IsEnabled = enabled;
                    tbTwitterKey.IsEnabled = enabled;
                    tbTwitterPIN.IsEnabled = enabled;
                    tbTwitterSecret.IsEnabled = enabled;
                    break;
            }
            
        }

        private void BtnGoogleKeyOK_Click(object sender, RoutedEventArgs e)
        {
            SetGoogleKey(tbGoogleKey.Text);

            tbGoogleKey.Clear();
        }
        
        private void BtnGoogleIdOK_Click(object sender, RoutedEventArgs e)
        {
            SetGoogleEngineID(tbGoogleID.Text);

            tbGoogleID.Clear();
        }

        private void BtnGoogleViewHidden_Click(object sender, RoutedEventArgs e)
        {
            if (IsGoogleEncrypt)
                runGoogleAPIKey.Text = GoogleAPIKey;
            else
                runGoogleAPIKey.Text = _hiddenText;

            IsGoogleEncrypt = !IsGoogleEncrypt;
        }

        #endregion

        #region [  Youtube  ]

        public VerifyType YoutubeVerifyType { get; internal set; }

        private bool IsYoutubeEncrypt { get; set; } = true;

        public string YoutubeAPIKey { get; private set; }

        public void SetYoutubeKey(string key)
        {
            SetYoutubeKey(key, false);
        }
        public void SetYoutubeKey(string key, bool systemInput)
        {
            if (key.IsNullOrEmpty())
            {
                tbYTMsg.Visibility = Visibility.Visible;
                tbYTMsg.Text = "API Key는 빈칸일 수 없습니다.";
                return;
            }

            tbYTMsg.Visibility = Visibility.Hidden;

            btnYTViewHidden.IsEnabled = true;

            YoutubeAPIKey = key;
            runYoutubeAPIKey.Text = _hiddenText;
            IsYoutubeEncrypt = true;

            if (!systemInput)
            {
                SettingManager.YoutubeCredentials.Item1 = key;
                SettingManager.YoutubeCredentials.Item2 = VerifyType.NotChecked;
            }
        }

        private void BtnYTOK_Click(object sender, RoutedEventArgs e)
        {
            SetYoutubeKey(tbYTKey.Text);

            tbYTKey.Clear();
        }

        private void BtnYTViewHidden_Click(object sender, RoutedEventArgs e)
        {
            if (IsYoutubeEncrypt)
                runYoutubeAPIKey.Text = YoutubeAPIKey;
            else
                runYoutubeAPIKey.Text = _hiddenText;

            IsYoutubeEncrypt = !IsYoutubeEncrypt;
        }

        #endregion
    }
}
