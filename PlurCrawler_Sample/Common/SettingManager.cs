using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Common;
using PlurCrawler.Extension;
using PlurCrawler.Search.Services.GoogleCSE;
using PlurCrawler.Search.Services.Twitter;

using PlurCrawler_Sample.Export;

using AppSetting = PlurCrawler_Sample.Properties.Settings;

namespace PlurCrawler_Sample.Common
{
    public static class SettingManager
    {
        public static void Init()
        {
            // Fake Method
        }

        static SettingManager()
        {
            _exportOptionSetting = new ObjectSerializer<ExportOptionSetting>().Deserialize(AppSetting.ExportOption);

            _googleCSESearchOption = new ObjectSerializer<GoogleCSESearchOption>().Deserialize(AppSetting.GoogleOption);
            _twitterSearchOption = new ObjectSerializer<TwitterSearchOption>().Deserialize(AppSetting.TwitterOption);

            if (AppSetting.GoogleCredentials.IsNullOrEmpty())
            {
                AppSetting.GoogleCredentials = "//";
                AppSetting.Save();
            }

            string[] gocredentials = AppSetting.GoogleCredentials.Split("//");

            _googleCredentials = new Pair<string, VerifyType, string, VerifyType>()
            {
                Item1 = gocredentials[0],
                Item2 = AppSetting.GoogleKeyVertified,
                Item3 = gocredentials[1],
                Item4 = AppSetting.GoogleIDVertified,
            };

            if (AppSetting.TwitterCredentials.IsNullOrEmpty())
            {
                AppSetting.TwitterCredentials = "//";
                AppSetting.Save();
            }

            string[] twcredentials = AppSetting.TwitterCredentials.Split("//");

            _twitterCredentials = new Pair<string, string>()
            {
                Item1 = twcredentials[0],
                Item2 = twcredentials[1],
            };

            if (AppSetting.EngineUsage.IsNullOrEmpty())
            {
                AppSetting.EngineUsage = "False|False|False";
                AppSetting.Save();
            }
            
            IEnumerable<bool> engineBools = AppSetting.Default.EngineUsage.Split("|")
                                                                          .Select(i => Convert.ToBoolean(i));

            _engineUsage = new Pair<bool, bool, bool>()
            {
                Item1 = engineBools.ElementAt(0),
                Item2 = engineBools.ElementAt(1),
                Item3 = engineBools.ElementAt(2),
            };

            // End Of Load

            ExportOptionSetting.PropertyChanged += ExportOptionSetting_PropertyChanged;
            GoogleCSESearchOption.PropertyChanged += GoogleCSESearchOption_PropertyChanged;
            TwitterSearchOption.PropertyChanged += TwitterSearchOption_PropertyChanged;
            EngineUsage.PropertyChanged += EngineUsage_PropertyChanged;
            GoogleCredentials.PropertyChanged += GoogleCredentials_PropertyChanged;

            TwitterCredentials.PropertyChanged += TwitterCredentials_PropertyChanged;
        }

        private static AppSetting AppSetting => AppSetting.Default;

        #region [  Event  ]

        private static void GoogleCSESearchOption_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var serializer = new ObjectSerializer<GoogleCSESearchOption>();
            AppSetting.GoogleOption = serializer.Serialize(GoogleCSESearchOption);

            AppSetting.Save();
        }

        private static void TwitterSearchOption_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var serializer = new ObjectSerializer<TwitterSearchOption>();
            AppSetting.TwitterOption = serializer.Serialize(TwitterSearchOption);

            AppSetting.Save();
        }
        
        private static void TwitterCredentials_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            AppSetting.Default.TwitterCredentials = $"{TwitterCredentials.Item1}//{TwitterCredentials.Item2}";

            AppSetting.Save();
        }

        private static void GoogleCredentials_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            AppSetting.GoogleCredentials = $"{GoogleCredentials.Item1}//{GoogleCredentials.Item3}";
            AppSetting.GoogleKeyVertified = GoogleCredentials.Item2;
            AppSetting.GoogleIDVertified = GoogleCredentials.Item4;

            AppSetting.Save();
        }

        private static void EngineUsage_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            AppSetting.EngineUsage = $@"{EngineUsage.Item1.ToString()
                                      }|{EngineUsage.Item2.ToString()
                                      }|{EngineUsage.Item3.ToString()}";

            AppSetting.Save();
        }

        private static void ExportOptionSetting_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var serializer = new ObjectSerializer<ExportOptionSetting>();
            AppSetting.ExportOption = serializer.Serialize(ExportOptionSetting);

            AppSetting.Save();
        }

        #endregion

        private static ExportOptionSetting _exportOptionSetting;

        public static ExportOptionSetting ExportOptionSetting
        {
            get => _exportOptionSetting;
            set
            {
                _exportOptionSetting = value;
                ExportOptionSetting_PropertyChanged(value, new PropertyChangedEventArgs("ExportOptionSetting"));
            }
        }
        
        private static GoogleCSESearchOption _googleCSESearchOption;

        public static GoogleCSESearchOption GoogleCSESearchOption
        {
            get => _googleCSESearchOption;
            set
            {
                _googleCSESearchOption = value;
                GoogleCSESearchOption_PropertyChanged(value, new PropertyChangedEventArgs("GoogleCSESearchOption"));
            }
        }

        private static TwitterSearchOption _twitterSearchOption;

        public static TwitterSearchOption TwitterSearchOption
        {
            get => _twitterSearchOption;
            set
            {
                _twitterSearchOption = value;
                TwitterSearchOption_PropertyChanged(value, new PropertyChangedEventArgs("TwitterSearchOption"));
            }
        }
        
        private static Pair<bool, bool, bool> _engineUsage;

        /// <summary>
        /// Google, Twitter, Youtube의 검색 사용 여부를 나타냅니다.
        /// </summary>
        public static Pair<bool, bool, bool> EngineUsage
        {
            get => _engineUsage;
            set
            {
                _engineUsage = value;
                EngineUsage_PropertyChanged(value, new PropertyChangedEventArgs("EngineUsage"));
            }
        }
        
        private static Pair<string, VerifyType, string, VerifyType> _googleCredentials;

        /// <summary>
        /// 구글 엔진의 API Key, Engine ID를 관리합니다.
        /// </summary>
        public static Pair<string, VerifyType, string, VerifyType> GoogleCredentials
        {
            get => _googleCredentials;
            set
            {
                _googleCredentials = value;
                GoogleCredentials_PropertyChanged(value, new PropertyChangedEventArgs("GoogleCredentials"));
            }
        }
        
        private static Pair<string, string> _twitterCredentials;

        /// <summary>
        /// 트위터 엔진의 Twitter Key와 Twitter Secret을 관리합니다.
        /// </summary>
        public static Pair<string, string> TwitterCredentials
        {
            get => _twitterCredentials;
            set
            {
                _twitterCredentials = value;
                TwitterCredentials_PropertyChanged(value, new PropertyChangedEventArgs("TwitterCredentials"));
            }
        }

    }
}

