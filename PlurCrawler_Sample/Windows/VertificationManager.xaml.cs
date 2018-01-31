using PlurCrawler_Sample.Enums;
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
        }
        
        private void BtnGoogleOK_Click(object sender, RoutedEventArgs e)
        {
            SetGoogleKey(tbGoogleKey.Text);
        }

        private void BtnGoogleViewHidden_Click(object sender, RoutedEventArgs e)
        {
            if (IsGoogleHidden)
                runGoogleAPIKey.Text = GoogleAPIKey;
            else
                runGoogleAPIKey.Text = HiddenText;

            IsGoogleHidden = !IsGoogleHidden;
        }


        #region [  Google CSE  ]

        private bool IsGoogleHidden { get; set; }

        public string GoogleAPIKey { get; private set; }

        public void SetGoogleKey(string key)
        {
            tbGoogleKey.Clear();

            GoogleAPIKey = key;
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

            runGoogleAvailable.Text = text;
        }

        #endregion
    }
}
