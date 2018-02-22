using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using PlurCrawler.Search.Base;
using PlurCrawler.Search;
using PlurCrawler.Tokens.Credentials;
using PlurCrawler.Search.Common;

using Google;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;

using YoutubeRequest = Google.Apis.YouTube.v3.SearchResource.ListRequest;
using PlurCrawler.Extension;

namespace PlurCrawler.Search.Services.Youtube
{
    public class YoutubeSearcher : BaseSearcher<YoutubeSearchOption, YoutubeSearchResult>
    {
        /// <summary>
        /// Api Key 입니다.
        /// </summary>
        public string ApiKey { get; private set; }
        
        /// <summary>
        /// ApiKey와 SearchEngineId를 새로 고칩니다. 유효성 검사는 하지 않습니다.
        /// </summary>
        /// <param name="apiKey">Api Key 입니다.</param>
        public void Vertification(string apiKey)
        {
            this.ApiKey = apiKey;
            IsVerification = true;
        }

        /// <summary>
        /// Youtube를 이용해 검색을 실시합니다.
        /// </summary>
        /// <param name="searchOption">Youtube의 검색 옵션입니다.</param>
        /// <returns></returns>
        public override IEnumerable<YoutubeSearchResult> Search(YoutubeSearchOption searchOption)
        {
            try
            {
                YoutubeRequest listRequest = BuildRequest(searchOption);
                List<YoutubeSearchResult> list = new List<YoutubeSearchResult>();

                int remain = searchOption.SearchCount;
                int count = 1;

                string nextToken = string.Empty;
                
                while (true)
                {
                    if (!nextToken.IsNullOrEmpty())
                    {
                        listRequest.PageToken = nextToken;
                    }

                    SearchListResponse searchResponse = listRequest.Execute();

                    nextToken = searchResponse.NextPageToken;

                    var videos = new List<SearchResult>();

                    foreach (SearchResult searchResult in searchResponse.Items)
                    {
                        OnChangeInfoMessage(this, new MessageEventArgs("비디오 타입을 분석중입니다."));
                        if (searchResult.Id.Kind == "youtube#video")
                        {
                            videos.Add(searchResult);
                            remain--;
                            OnSearchProgressChanged(this, new ProgressEventArgs(searchOption.SearchCount, count++));
                        }
                        if (remain <= 0)
                            break;
                        
                    }

                    count = 1;
                    
                    foreach (SearchResult s in videos)
                    {
                        OnChangeInfoMessage(this, new MessageEventArgs("설명을 가져오는 중입니다."));
                        OnSearchProgressChanged(this, new ProgressEventArgs(searchOption.SearchCount, count++));

                        string title = s.Snippet.Title;
                        string description = s.Snippet.Description;

                        if (description.EndsWith("..."))
                            description = GetFullDescription(s.Id.VideoId);

                        var itm = new YoutubeSearchResult()
                        {
                            Title = title,
                            OriginalURL = $"{"http:"}//youtube.com/watch?v={s.Id.VideoId}",
                            PublishedDate = s.Snippet.PublishedAt,
                            ChannelTitle = s.Snippet.ChannelTitle,
                            Description = description,
                            ChannelId = s.Snippet.ChannelId,
                            VideoId = s.Id.VideoId,
                            Keyword = searchOption.Query,
                        };

                        OnSearchItemFound(this, new SearchResultEventArgs(itm, ServiceKind.Youtube));

                        list.Add(itm);
                    }

                    if (remain <= 0)
                        break;
                }
                OnSearchFinished(this);

                return list;
            }
            catch (GoogleApiException ex)
            {
                if (ex.Message.Contains("keyInvalid"))
                    throw new CredentialsTypeException("키가 올바르게 입력되지 않았습니다.");
                else
                    throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public YoutubeRequest BuildRequest(YoutubeSearchOption searchOption)
        {
            YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = ApiKey
            });
            
            YoutubeRequest listRequest = youtube.Search.List("snippet");
            listRequest.Q = searchOption.Query;

            if (searchOption.UseDateSearch)
            {
                listRequest.PublishedAfter = searchOption.DateRange.Since;
                listRequest.PublishedBefore = searchOption.DateRange.Until;
            }
            
            listRequest.Order = YoutubeRequest.OrderEnum.Relevance;

            int count = searchOption.SearchCount;
            
            listRequest.MaxResults = count <= 50 ? count : 50;
            listRequest.SafeSearch = YoutubeRequest.SafeSearchEnum.Strict;

            return listRequest;
        }

        #region [  Utility  ]

        private byte[] ToByteArray(string hexString)
        {
            int NumberChars = hexString.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return bytes;
        }

        private string ToUnicode(string _longHexString)
        {
            return char.ConvertFromUtf32(Convert.ToInt32(_longHexString, 16));
        }

        private string GetFullDescription(string videoId)
        {
            string url = $"{"https"}://m.youtube.com/watch?v={videoId}";

            WebClient client = new WebClient();

            // Mobile로 인식하기 위한 헤더
            client.Headers.Add("user-agent", "Opera/12.02 (Android 4.1; Linux; Opera Mobi/ADR-1111101157; U; en-US) Presto/2.9.201 Version/12.02");

            string code = client.DownloadString(url);

            string mainPattern = @"(?<=\\""text\\"": \\"").+?(?=\\"")";
            string subPattern = @"\\\\u([\da-f]{2})([\da-f]{2})";
            string subPattern2 = @"\\\\U([0-9a-f]{8})";

            code = code.Split(new string[] { "\\\"description" }, StringSplitOptions.None)[1];
            code = code.Split(new string[] { "\\\"runs\\\"" }, StringSplitOptions.None)[1];

            StringBuilder sb = new StringBuilder();

            foreach (Match m in Regex.Matches(code, mainPattern))
            {
                string temp = m.Value;

                foreach (Match m2 in Regex.Matches(m.Value, subPattern))
                {
                    byte[] bytes = ToByteArray(m2.Groups[2].Value + m2.Groups[1].Value);

                    temp = temp.Replace(m2.Value, new string(Encoding.Unicode.GetChars(bytes)));
                }

                foreach (Match m2 in Regex.Matches(m.Value, subPattern2))
                {
                    temp = temp.Replace(m2.Value, ToUnicode(m2.Groups[1].Value));
                }

                temp = temp.Replace(@"\\n", Environment.NewLine);
                temp = temp.Replace(@"\\\/", "/");
                sb.Append(temp);
            }

            return sb.ToString();
        }

        #endregion
    }
}
