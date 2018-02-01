using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlurCrawler.Search.Base;
using Tweetinvi.Models;
using Tweetinvi.Parameters;

using ITweetSearchResult = Tweetinvi.Models.ISearchResult;

namespace PlurCrawler.Search.Services.Twitter
{
    public class TwitterSearcher : BaseSearcher<TwitterSearchOption, TwitterSearchResult>
    {
        /// <summary>
        /// 인증 상태를 확인합니다.
        /// </summary>
        public new bool IsVerification => Tweetinvi.Auth.Credentials != null;

        /// <summary>
        /// Twitter를 이용해 검색을 실시합니다.
        /// </summary>
        /// <param name="searchOption">트위터의 검색 옵션입니다.</param>
        /// <returns></returns>
        public override IEnumerable<TwitterSearchResult> Search(TwitterSearchOption searchOption)
        {
            int maxid = 0;

            IEnumerable<TwitterSearchResult> result = new List<TwitterSearchResult>();

            if (searchOption.SplitWithDate)
            {
                if (searchOption.DateRange.Since != null)
                {
                    DateTime time = searchOption.DateRange.Since.Value.Date;
                    do
                    {
                        result = result.Union(SearchOneDay(time));
                        
                        if (time.Date == searchOption.DateRange.Until.Value.Date)
                            break;

                        time = time.AddDays(1);

                    } while (true);

                    return result;
                }

                return null;
            }
            else
            {
                // 날짜와 관계 없이 전체 검색
                int searchCount = (int)searchOption.SearchCount;
                
                while (searchCount > 0)
                {
                    int count = GetSearchCount(searchCount);
                    result = result.Union(Search(count, maxid));

                    if (maxid == -1)
                        break;

                    searchCount -= count;
                }

                return result.ToList();
            }

            // 내부 함수 - 날짜에 다른 검색 [offset은 본 함수에서 호출]
            IEnumerable<TwitterSearchResult> SearchOneDay(DateTime time)
            {
                int searchCount = (int)searchOption.SearchCount;

                IEnumerable<TwitterSearchResult> tweetList = new List<TwitterSearchResult>();

                while (searchCount > 0)
                {
                    int count = GetSearchCount(searchCount);
                    var list = Search(count, maxid, time);

                    if (list.Count() == 0)
                        break;

                    tweetList = tweetList.Union(list);
                }

                return tweetList.ToList();
            }

            // 내부 함수 - 갯수에 따른 검색 [최대 100개까지 호출 가능 // MaxId로 구분]
            IEnumerable<TwitterSearchResult> Search(int searchCount, int maxId, DateTime time = default(DateTime))
            {
                var searchParam = new SearchTweetsParameters(searchOption.Query)
                {
                    MaximumNumberOfResults = searchCount,
                    Since = searchOption.DateRange.Since.GetValueOrDefault(),
                    Until = searchOption.DateRange.Until.GetValueOrDefault(),
                    MaxId = maxid
                };

                if (time != default(DateTime))
                {
                    searchParam.Since = time;
                    searchParam.Until = time.AddDays(1);
                }

                ITweetSearchResult results = Tweetinvi.Search.SearchTweetsWithMetadata(searchParam);

                if (results.Tweets != null)
                {
                    IEnumerable<TwitterSearchResult> tweets = results.Tweets
                        .Select(i => ConvertResult(i));

                    return tweets;
                }

                return new List<TwitterSearchResult>();
            }
        }

        private int GetSearchCount(int searchCount)
        {
            if (searchCount > 100)
                return 100;

            return searchCount;
        }

        private TwitterSearchResult ConvertResult(ITweetWithSearchMetadata tweet)
        {
            return new TwitterSearchResult()
            {
                Title = tweet.CreatedBy.Name + FormatTitle(tweet.Text),
                IsRetweeted = tweet.Retweeted,
                PublishedDate = tweet.CreatedAt,
                Text = tweet.FullText,
                CreatorName = tweet.CreatedBy.Name,
                CreatorId = tweet.CreatedBy.ScreenName,
                OriginalURL = tweet.Url
            };
        }

        private string FormatTitle(string text)
        {
            if (text.Length > 10)
                return text.Substring(0, 10);

            return text;
        }
    }
}
