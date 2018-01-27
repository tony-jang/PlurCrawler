using System;
using System.Collections.Generic;
using System.Linq;

using PlurCrawler.Search.Base;
using PlurCrawler.Search;

using Google.Apis.Customsearch.v1;
using Google.Apis.Customsearch.v1.Data;
using Google.Apis.Services;

namespace PlurCrawler.Search.Services.GoogleCSE
{
    public class GoogleCSESearcher : BaseSearcher
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
            this.ApiKey = apiKey;
            this.SearchEngineId = searchEngineId;
                
            IsVerification = true;
        }

        /// <summary>
        /// Google Custom Search를 이용해 검색을 실시합니다. <see cref="BaseSearcher"/>에서 상속 받은 함수 입니다.
        /// </summary>
        /// <param name="searchOption">구글 검색의 검색 옵션입니다. <see cref="GoogleCSESearchOption"/>이 필요합니다.</param>
        /// <returns></returns>
        public override List<ISearchResult> Search(IDateSearchOption searchOption)
        {
            if (searchOption is GoogleCSESearchOption googleSearchOption)
            {
                return Search(googleSearchOption).Select(i => (ISearchResult)i).ToList();
            }
            else
            {
                throw new SearchOptionTypeException("GoogleCSESearchOption만 넣을 수 있습니다.");
            }
        }

        /// <summary>
        /// Google Custom Search를 이용해 검색을 실시합니다.
        /// </summary>
        /// <param name="searchOption">Google Custom Search의 검색 옵션입니다.</param>
        /// <returns></returns>
        public List<GoogleCSESearchResult> Search(GoogleCSESearchOption searchOption)
        {
            var list = new List<GoogleCSESearchResult>();
            var customSearchService = new CustomsearchService(new BaseClientService.Initializer
            {
                ApiKey = this.ApiKey
            });

            long targetCount = (long)searchOption.SearchCount;

            CseResource.ListRequest request = customSearchService.Cse.List(searchOption.Query);

            request.Cx = SearchEngineId;

            var results = new List<Result>();
            IList<Result> paging = new List<Result>();
            int count = 0;

            while ((paging != null) && targetCount > 0)
            {
                request.Start = count * 10 + 1;
                if ((targetCount % 10) == 0)
                    request.Num = 10;
                else
                    request.Num = (targetCount % 10);

                paging = request.Execute().Items;

                if (paging != null)
                    results.AddRange(paging);

                count++;
                targetCount -= request.Num.Value;
            }

            if (results.Count == 0)
                return null;

            return results.Select(i => new GoogleCSESearchResult()
            {
                OriginalURL = i.Link,
                PublishedDate = null,
                Title = i.Title,
                Snippet = i.Snippet.Replace("\\n", Environment.NewLine)
            }).ToList();
        }
    }
}
