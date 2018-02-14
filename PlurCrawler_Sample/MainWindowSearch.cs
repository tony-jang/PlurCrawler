using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using PlurCrawler.Search;
using PlurCrawler.Search.Base;
using PlurCrawler.Search.Common;
using PlurCrawler.Search.Services.GoogleCSE;
using PlurCrawler.Format.Common;
using PlurCrawler.Extension;

using PlurCrawler_Sample.Common;
using PlurCrawler_Sample.Controls;
using PlurCrawler_Sample.Export;
using PlurCrawler_Sample.TaskLogs;
using PlurCrawler_Sample.Report;

namespace PlurCrawler_Sample
{
    public partial class MainWindow
    {
        bool googleSearching = false,
            youtubeSearching = false,
            twitterSearching = false;

        public void AddLog(string message, TaskLogType type)
        {
            Dispatcher.Invoke(() =>
            {
                if (mainTabControl.SelectedIndex != 0)
                    mainTabControl.SelectedIndex = 0;
                _logManager.AddLog(message, type);
            });
            
        }

        #region [  Google CSE  ]

        public void SearchGoogle()
        {
            // 동일한 서비스는 끝나기 전까지 실행이 불가능함.
            if (googleSearching)
                return;

            googleSearching = true;
            _detailsOption.GoogleEnableChange(false);
            _vertManager.ChangeEditable(false);

            Thread thr = new Thread(() =>
            {
                var googleCSESearcher = new GoogleCSESearcher();
                bool isCanceled = false;
                SearchResult searchResultInfo = SearchResult.Fail_APIError;
                GoogleCSESearchOption option = null;

                googleCSESearcher.SearchProgressChanged += GoogleCSESearcher_SearchProgressChanged;
                googleCSESearcher.SearchFinished += GoogleCSESearcher_SearchFinished;

                Dispatcher.Invoke(() =>
                {
                    AddLog("Google CSE 검색 엔진을 초기화중입니다.", TaskLogType.SearchReady);

                    // 옵션 초기화
                    option = _detailsOption.GetGoogleCSESearchOption();
                    option.Query = tbQuery.Text;

                    var tb = new TaskProgressBar();

                    tb.SetValue(title: "Google CSE 검색", message: "검색이 진행중입니다.", maximum: option.SearchCount);

                    lvTask.Items.Add(tb);
                    dict[googleCSESearcher] = tb;

                    if (option.OutputServices == OutputFormat.None)
                    {
                        tb.SetValue(message: "결과를 내보낼 위치가 없습니다.", maximum: 1);
                        AddLog("검색을 내보낼 위치가 없습니다.", TaskLogType.Failed);

                        searchResultInfo = SearchResult.Fail_InvaildSetting;
                        isCanceled = true;
                    }

                    googleCSESearcher.Vertification(_vertManager.GoogleAPIKey,
                        _vertManager.GoogleEngineID);

                    if (!googleCSESearcher.IsVerification) // 인증되지 않았을 경우
                    {
                        tb.SetValue(message: "API키가 인증되지 않았습니다.", maximum: 1);
                        AddLog("API키가 인증되지 않았습니다.", TaskLogType.Failed);

                        searchResultInfo = SearchResult.Fail_APIError;
                        isCanceled = true;
                    }
                });

                IEnumerable<GoogleCSESearchResult> googleResult = null;

                if (!isCanceled)
                {
                    try
                    {
                        googleResult = googleCSESearcher.Search(option);
                        searchResultInfo = SearchResult.Success;
                        Export(option, googleResult);
                    }
                    catch (InvaildOptionException)
                    {
                        AddLog("'Google CSE' 검색 중 오류가 발생했습니다. [날짜를 사용하지 않은 상태에서는 '하루 기준' 옵션을 사용할 수 없습니다.]", TaskLogType.Failed);
                        searchResultInfo = SearchResult.Fail_InvaildSetting;
                    }
                }

                Dispatcher.Invoke(() => {

                    _taskReport.AddReport(new TaskReportData()
                    {
                        Query = option.Query,
                        RequestService = ServiceKind.GoogleCSE,
                        SearchCount = option.SearchCount,
                        SearchData = googleResult,
                        SearchDate = DateTime.Now,
                        SearchResult = searchResultInfo,
                    });

                    _taskReport.SetLastReport();

                    googleSearching = false;
                    _detailsOption.GoogleEnableChange(true);
                    _vertManager.ChangeEditable(true);
                });
            });
            
            thr.Start();
        }

        private void GoogleCSESearcher_SearchFinished(object sender)
        {
            Dispatcher.Invoke(() =>
            {
                TaskProgressBar itm = dict[sender as ISearcher];
                itm.SetValue(value: itm.Maximum, message: "검색이 완료되었습니다.");

                _logManager.AddLog("검색이 완료되었습니다.", TaskLogType.Searching);

                _vertManager.ChangeGoogleState(VerifyType.Verified, true);
                _vertManager.ChangeGoogleState(VerifyType.Verified, false);
            });
        }

        private void GoogleCSESearcher_SearchProgressChanged(object sender, ProgressEventArgs args)
        {
            Dispatcher.Invoke(() =>
            {
                var itm = dict[sender as ISearcher];
                itm.SetValue(maximum: args.Maximum, value: args.Value);
            });
        }

        #endregion

        public void Export(ISearchOption option, IEnumerable<ISearchResult> result)
        {
            Dispatcher.Invoke(() =>
            {
                if (option.OutputServices.HasFlag(OutputFormat.Json))
                {
                    string fullPath = _exportOption.JsonFullPath;
                    string folder = _exportOption.JsonFolderPath;
                    string fileName = _exportOption.JsonFileName;

                    if (fileName.IsNullOrEmpty())
                    {
                        AddLog("파일을 Json 형식으로 내보내기 실패했습니다. [파일명 입력란이 비어있습니다.]", TaskLogType.Failed);
                        
                    }
                    else
                    {
                        if (Directory.Exists(folder))
                        {
                            ExportManager.JsonExport(fullPath, result);
                        }
                        else
                        {
                            if (folder.IsNullOrEmpty())
                            {
                                AddLog("파일을 Json 형식으로 내보내기 실패했습니다. [폴더 입력란이 비어있습니다.]", TaskLogType.Failed);
                            }
                            else if (!Directory.Exists(folder))
                            {
                                AddLog("파일을 Json 형식으로 내보내기 실패했습니다. [해당하는 경로가 없습니다.]", TaskLogType.Failed);
                            }
                        }
                    }
                }
                if (option.OutputServices.HasFlag(OutputFormat.CSV))
                {
                    string fullPath = _exportOption.CSVFullPath;
                    string folder = _exportOption.CSVFolderPath;
                    string fileName = _exportOption.JsonFileName;

                    if (fileName.IsNullOrEmpty())
                    {
                        AddLog("파일을 CSV 형식으로 내보내기 실패했습니다. [파일명 입력란이 비어있습니다.]", TaskLogType.Failed);
                    }
                    else
                    {
                        if (Directory.Exists(folder))
                        {
                            ExportManager.CSVExport(fullPath, result);
                        }
                        else
                        {
                            if (folder.IsNullOrEmpty())
                            {
                                AddLog("파일을 CSV 형식으로 내보내기 실패했습니다. [폴더 입력란이 비어있습니다.]", TaskLogType.Failed);
                            }
                            else if (!Directory.Exists(folder))
                            {
                                AddLog("파일을 CSV 형식으로 내보내기 실패했습니다. [해당하는 경로가 없습니다.]", TaskLogType.Failed);
                            }
                        }
                    }
                }
                if (option.OutputServices.HasFlag(OutputFormat.MySQL))
                {

                }
                if (option.OutputServices.HasFlag(OutputFormat.AccessDB))
                {
                    // TODO: Implements
                }
            });
        }


    }
}
