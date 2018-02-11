using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using PlurCrawler.Format.Common;
using PlurCrawler.Search.Common;
using PlurCrawler.Search.Services.GoogleCSE;

using PlurCrawler_Sample.Controls;
using PlurCrawler_Sample.Export;
using PlurCrawler_Sample.TaskLogs;

namespace PlurCrawler_Sample.Search
{
    public class GoogleCSESearchHelper
    {
        public GoogleCSESearchHelper(string googleAPIKey, string googleEngineID,
            GoogleCSESearchOption option, TaskLogManager manager, TaskProgressBar bar)
        {
            this.GoogleAPIKey = googleAPIKey;
            this.GoogleEngineID = googleEngineID;
            this.Option = option;
            this.LogManager = manager;
            this.ProgressBar = bar;
        }

        private static bool IsSearching { get; set; }// 검색중인지에 대한 여부 확인

        private string GoogleAPIKey { get; set; }

        private string GoogleEngineID { get; set; }

        private TaskLogManager LogManager { get; set; }

        private GoogleCSESearchOption Option { get; set; }

        private TaskProgressBar ProgressBar { get; set; }

        private Dispatcher Dispatcher => Common.GlobalData.MainWindowDispatcher;

        public void StartSearch()
        {
            if (IsSearching)
            {
                LogManager.AddLog("'Google CSE' 엔진에서 이미 검색이 진행중입니다.", TaskLogType.SearchFailed);
            }
            IsSearching = true;

            LogManager.AddLog("Google CSE 검색 엔진을 초기화중입니다.", TaskLogType.SearchReady);

            SetProgressBarValue(title: "Google CSE 검색", message: "엔진 초기화중입니다..", maximum: Option.SearchCount);

            GoogleCSESearcher searcher = new GoogleCSESearcher();
            bool flag = true;

            searcher.SearchFinished += Searcher_SearchFinished;
            searcher.SearchProgressChanged += Searcher_SearchProgressChanged;

            searcher.Vertification(GoogleAPIKey, GoogleEngineID);

            if (!searcher.IsVerification) // 유효성 검사는 하지 않지만 기본적으로 비어있는지를 확인함
            {
                LogManager.AddLog("Google CSE API 또는 Engine ID가 비어있습니다.", TaskLogType.SearchFailed);
                SetProgressBarValue(message: "API Key가 인증되지 않았습니다.", maximum: 1);
                flag = false;
            }

            if (Option.OutputServices == OutputFormat.None)
            {
                LogManager.AddLog("검색 결과를 내보낼 위치가 없습니다. 검색을 진행하지 않습니다.", TaskLogType.SearchFailed);
                SetProgressBarValue(message: "결과를 내보낼 위치가 없습니다.", maximum: 1);
                flag = false;
            }
            
            if (flag)
            {
                IEnumerable<GoogleCSESearchResult> result = searcher.Search(Option);

                ExportManager.CSVExport(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "TestFile.csv"), result);
                // 내보내기
            }

            IsSearching = false;
        }

        private void Searcher_SearchProgressChanged(object sender, ProgressEventArgs args)
        {
            SetProgressBarValue(maximum: args.Maximum, value: args.Value);
        }

        private void SetProgressBarValue(string title = null, string message = null, double value = -1, double maximum = -1)
        {
            ProgressBar.SetValue(title, message, value, maximum);
        }

        private void Searcher_SearchFinished(object sender)
        {
            SetProgressBarValue(message: "검색이 완료되었습니다.", value: ProgressBar.Maximum);
        }

        /*
        필요한 기능

        - TaskProgressBar에 접근
        - LogItem에 접근
        - VertificationManager에 접근

        3가지를 이벤트로 해결 [TaskProgressBar]

        - 검색의 주체는 해당 Helper가 처리
        - 

        */
    }
}
