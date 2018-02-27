using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

using PlurCrawler.Search.Base;
using PlurCrawler.Search;
using PlurCrawler.Search.Common;
using PlurCrawler.Extension;

using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;

using static PlurCrawler.Extension.IEnumerableEx;

namespace PlurCrawler.Search.Services.GoogleCSE
{
    public class GoogleCSESearcher : BaseSearcher<GoogleCSESearchOption, GoogleCSESearchResult>
    {
        /// <summary>
        /// <see cref="GoogleCSESearcher"/> 클래스를 초기화합니다.
        /// </summary>
        public GoogleCSESearcher()
        {
        }

        /// <summary>
        /// <see cref="GoogleCSESearcher"/> 클래스를 API Key와 검색 엔진 ID로 초기화합니다.
        /// </summary>
        /// <param name="apiKey">API Key 입니다.</param>
        /// <param name="searchEngineId">검색 엔진 ID입니다.</param>
        public GoogleCSESearcher(string apiKey, string searchEngineId)
        {
            Vertification(apiKey, searchEngineId);
        }
        
        /// <summary>
        /// Api Key 입니다.
        /// </summary>
        public string ApiKey { get; private set; }

        /// <summary>
        /// 검색 엔진 ID 입니다.
        /// </summary>
        public string SearchEngineId { get; private set; }
        
        /// <summary>
        /// ApiKey와 SearchEngineId를 새로 고칩니다. 유효성 검사는 하지 않습니다.
        /// </summary>
        /// <param name="apiKey">Api Key 입니다.</param>
        /// <param name="searchEngineId">검색 엔진 ID 입니다.</param>
        public void Vertification(string apiKey, string searchEngineId)
        {
            if (apiKey.IsNullOrEmpty() || searchEngineId.IsNullOrEmpty())
                return;

            this.ApiKey = apiKey;
            this.SearchEngineId = searchEngineId;

            IsVerification = true;
        }
        
        private int Maximum { get; set; } = 0;
        private int Current { get; set; } = 0;

        private string query { get; set; }

        /// <summary>
        /// Google Custom Search를 이용해 검색을 실시합니다.
        /// </summary>
        /// <param name="searchOption">Google Custom Search의 검색 옵션입니다.</param>
        /// <returns></returns>
        public override IEnumerable<GoogleCSESearchResult> Search(GoogleCSESearchOption searchOption)
        {
            query = searchOption.Query;

            CseResource.ListRequest request = BuildRequest(searchOption);

            if (!searchOption.SplitWithDate)
            {
                Maximum = searchOption.SearchCount;

                IEnumerable<Result> results = Search(request, searchOption.Offset, searchOption.SearchCount);

                OnSearchFinished(this, new SearchFinishedEventArgs(ServiceKind.GoogleCSE));

                if (results.Count() == 0)
                    return Enumerable.Empty<GoogleCSESearchResult>();

                return ConvertType(results);
            }
            else
            {
                Maximum = searchOption.SearchCount * searchOption.DateRange.GetDateRange().Count();

                if (!searchOption.UseDateSearch) // DateSearch가 아닌데 날짜별로 구분할 수 없으므로 예외 발생
                    throw new InvaildOptionException("날짜를 사용하지 않는데 날짜별로 구분해 검색할 수 없습니다.");

                var results = new List<Result>();
                
                foreach(DateTime dt in searchOption.DateRange.GetDateRange())
                {
                    request.Sort = $"date:r:{dt.To8LengthYear()}:{dt.To8LengthYear()}";
                    results.AddRange(Search(request, searchOption.Offset, searchOption.SearchCount, dt));
                }
                
                return ConvertType(results);
            }
        }

        #region [  Private Method  ]

        private CseResource.ListRequest BuildRequest(GoogleCSESearchOption option)
        {
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer
            {
                ApiKey = this.ApiKey
            });

            CseResource.ListRequest request = customSearchService.Cse.List(option.Query);

            request.Cx = SearchEngineId;
            request.Start = option.Offset;
            if (option.CountryCode != CountryRestrictsCode.All)
            {
                request.Cr = option.CountryCode.ToString();
            }
            
            if (option.UseDateSearch)
                request.Sort = $@"date:r:{option.DateRange.Since.GetValueOrDefault().To8LengthYear()
                                       }:{option.DateRange.Until.GetValueOrDefault().To8LengthYear()}";

            return request;
        }
        
        private IEnumerable<Result> Search(CseResource.ListRequest request, int offset, int targetCount, DateTime? dt = null)
        {
            var results = new List<Result>();
            IList<Result> paging = new List<Result>();
            int count = 0;
            long tempCount = targetCount;
            
            while ((paging != null) && tempCount > 0)
            {
                request.Start = count * 10 + 1 + offset;
                if ((tempCount % 10) == 0)
                    request.Num = 10;
                else
                    request.Num = (tempCount % 10);

                paging = request.Execute().Items;

                if (paging != null)
                {
                    results.AddRange(paging);
                    var itm = paging.Select(i => ConvertType(i, dt));
                    itm.ForEach(i => OnSearchItemFound(this, new SearchResultEventArgs(i, ServiceKind.GoogleCSE)));
                }

                count++;

                tempCount -= request.Num.Value;
                Current += (int)request.Num.Value;

                int currCount = (int)(targetCount - tempCount);
                OnSearchProgressChanged(this, new ProgressEventArgs(Maximum, Current));
            }

            return results;
        }

        private IEnumerable<GoogleCSESearchResult> ConvertType(IEnumerable<Result> resultList)
        {
            return resultList.Select(i => new GoogleCSESearchResult()
            {
                OriginalURL = i.Link,
                PublishedDate = null,
                Title = i.Title,
                Snippet = i.Snippet.Replace("\\n", Environment.NewLine),
                Keyword = query
            });
        }

        private GoogleCSESearchResult ConvertType(Result result, DateTime? date)
        {
            return new GoogleCSESearchResult()
            {
                OriginalURL = result.Link,
                PublishedDate = date,
                Title = result.Title,
                Snippet = result.Snippet.Replace("\\n", Environment.NewLine),
                Keyword = query
            };
        }

        #endregion
        
    }
}
