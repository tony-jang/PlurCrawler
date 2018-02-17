using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using PlurCrawler_Sample.Common;
using PlurCrawler_Sample.Controls;
using PlurCrawler_Sample.Export;
using PlurCrawler_Sample.Report;
using PlurCrawler_Sample.Report.Result;
using PlurCrawler_Sample.TaskLogs;

using PlurCrawler.Extension;
using PlurCrawler.Format.Common;
using PlurCrawler.Search;
using PlurCrawler.Search.Base;
using PlurCrawler.Search.Common;
using PlurCrawler.Search.Services.GoogleCSE;

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
            _vertManager.ChangeEditable(false, ServiceKind.GoogleCSE);

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
                ExportResultPack pack = null;

                if (!isCanceled)
                {
                    try
                    {
                        googleResult = googleCSESearcher.Search(option);
                        searchResultInfo = SearchResult.Success;
                        AddLog("검색 결과를 내보내는 중입니다.", TaskLogType.Searching);
                        pack = Export(option.OutputServices, googleResult);
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
                        OutputFormat = option.OutputServices,
                        ExportResultPack = pack
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

        public void TwitterSearch()
        {
            if (twitterSearching)
                return;

            twitterSearching = true;
            _detailsOption.TwitterEnableChange(false);
            _vertManager.ChangeEditable(false, ServiceKind.Twitter);

            // TODO: 
        }

        public ExportResultPack Export(OutputFormat format, IEnumerable<ISearchResult> result)
        {
            ExportResultPack pack = null;
            Dispatcher.Invoke(() =>
            {
                pack = new ExportResultPack();

                if (format.HasFlag(OutputFormat.Json))
                {
                    string fullPath = _exportOption.JsonFullPath;
                    string folder = _exportOption.JsonFolderPath;
                    string fileName = _exportOption.JsonFileName;

                    if (fileName.IsNullOrEmpty())
                    {
                        AddLog("파일을 Json 형식으로 내보내기 실패했습니다. [파일명 입력란이 비어있습니다.]", TaskLogType.Failed);
                        pack.JsonExportResult = JsonExportResult.Fail_FileNameNull;
                    }
                    else
                    {
                        if (Directory.Exists(folder))
                        {
                            ExportManager.JsonExport(fullPath, result, _exportOption.UseJsonSort);
                            AddLog($"Json으로 성공적으로 내보냈습니다. 저장 위치 : {fullPath}", TaskLogType.Complete);
                            pack.JsonExportResult = JsonExportResult.Success;
                        }
                        else
                        {
                            if (folder.IsNullOrEmpty())
                            {
                                AddLog("파일을 Json 형식으로 내보내기 실패했습니다. [폴더 입력란이 비어있습니다.]", TaskLogType.Failed);
                                pack.JsonExportResult = JsonExportResult.Fail_FileDirectoryNull;
                            }
                            else if (!Directory.Exists(folder))
                            {
                                AddLog("파일을 Json 형식으로 내보내기 실패했습니다. [해당하는 경로가 없습니다.]", TaskLogType.Failed);
                                pack.JsonExportResult = JsonExportResult.Fail_FileDirectoryNotExists;
                            }
                        }
                    }
                }
                if (format.HasFlag(OutputFormat.CSV))
                {
                    string fullPath = _exportOption.CSVFullPath;
                    string folder = _exportOption.CSVFolderPath;
                    string fileName = _exportOption.JsonFileName;

                    if (fileName.IsNullOrEmpty())
                    {
                        AddLog("파일을 CSV 형식으로 내보내기 실패했습니다. [파일명 입력란이 비어있습니다.]", TaskLogType.Failed);
                        pack.CSVExportResult = CSVExportResult.Fail_FileNameNull;
                    }
                    else
                    {
                        if (Directory.Exists(folder))
                        {
                            ExportManager.CSVExport(fullPath, result);
                            AddLog($"CSV로 성공적으로 내보냈습니다. 저장 위치 : {fullPath}", TaskLogType.Complete);
                            pack.CSVExportResult = CSVExportResult.Success;
                        }
                        else
                        {
                            if (folder.IsNullOrEmpty())
                            {
                                AddLog("파일을 CSV 형식으로 내보내기 실패했습니다. [폴더 입력란이 비어있습니다.]", TaskLogType.Failed);
                                pack.CSVExportResult = CSVExportResult.Fail_FileDirectoryNull;
                            }
                            else if (!Directory.Exists(folder))
                            {
                                AddLog("파일을 CSV 형식으로 내보내기 실패했습니다. [해당하는 경로가 없습니다.]", TaskLogType.Failed);
                                pack.CSVExportResult = CSVExportResult.Fail_FileDirectoryNotExists;
                            }
                        }
                    }
                }
                if (format.HasFlag(OutputFormat.MySQL))
                {

                }
                if (format.HasFlag(OutputFormat.AccessDB))
                {
                    // TODO: Implements
                }
            });

            return pack;
        }
    }
}
