using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using PlurCrawler.Search.Base;
using PlurCrawler.Search;

namespace PlurCrawler.Search.Services.Youtube
{
    public class YoutubeSearcher : BaseSearcher
    {

        public override List<ISearchResult> Search(IDateSearchOption searchOption)
        {
            if (searchOption is YoutubeSearchOption searcher)
            {
                return null;
            }
            else
            {
                throw new SearchOptionTypeException("GoogleCSESearchOption만 넣을 수 있습니다.");
            }
        }

        private byte[] ToByteArray(String HexString)
        {
            int NumberChars = HexString.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 16);
            }
            return bytes;
        }

        private string GetFullDescription(string videoId)
        {
            string url = $"https://m.youtube.com/watch?v={videoId}";

            WebClient client = new WebClient();

            // Mobile로 인식하기 위한 헤더
            client.Headers.Add("user-agent", "Opera/12.02 (Android 4.1; Linux; Opera Mobi/ADR-1111101157; U; en-US) Presto/2.9.201 Version/12.02");

            string code = client.DownloadString(url);


            string mainPattern = @"(?<=\\""text\\"": \\"").+?(?=\\"")";
            string subPattern = @"\\\\u([\da-f]{2})([\da-f]{2})";

            code = code.Split(new string[] { "\\\"description" }, StringSplitOptions.None)[1];
            code = code.Split(new string[] { "\\\"runs\\\"" }, StringSplitOptions.None)[1];

            StringBuilder sb = new StringBuilder();

            foreach (Match m in Regex.Matches(code, mainPattern))
            {
                string temp = m.Value;

                foreach (Match m2 in Regex.Matches(m.Value, subPattern))
                {
                    StringBuilder sb2 = new StringBuilder();
                    
                    byte[] bytes = ToByteArray(m2.Groups[2].Value + m2.Groups[1].Value);

                    temp = temp.Replace(m2.Value, new string(Encoding.Unicode.GetChars(bytes)));
                }

                temp = temp.Replace(@"\\n", Environment.NewLine);
                temp = temp.Replace(@"\\\/", "/");
                sb.Append(temp);
            }

            return sb.ToString();
        }


        public List<YoutubeSearchResult> Search(YoutubeSearchOption searchOption)
        {
            YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "AIzaSyDuJa0F9nJzUwTOJyPZeBqkbEPwHoxwTG4"
            });

            SearchResource.ListRequest listRequest = youtube.Search.List("snippet");
            listRequest.Q = searchOption.Query;
            listRequest.PublishedAfter = searchOption.PublishedDateRange.StartTime;
            listRequest.PublishedBefore = searchOption.PublishedDateRange.EndTime;

            listRequest.Order = SearchResource.ListRequest.OrderEnum.Relevance;
            listRequest.MaxResults = 25;
            listRequest.SafeSearch = SearchResource.ListRequest.SafeSearchEnum.Strict;
            SearchListResponse searchResponse = listRequest.Execute();

            var videos = new List<SearchResult>();
            var channels = new List<string>();
            var playlists = new List<string>();

            foreach (SearchResult searchResult in searchResponse.Items)
            {
                // youtube#channel
                // youtube#playlist
                if (searchResult.Id.Kind == "youtube#video")
                    videos.Add(searchResult);
            }
            List<YoutubeSearchResult> list = new List<YoutubeSearchResult>();
            foreach (SearchResult s in videos)
            {
                string title = s.Snippet.Title;
                string description = s.Snippet.Description;

                if (description.EndsWith("..."))
                {
                    description = GetFullDescription(s.Id.VideoId) + "\n[Test :: FullDescription Called]";
                }

                list.Add(new YoutubeSearchResult()
                {
                    Title = title,
                    OriginalURL = "http://youtube.com/watch?v=" + s.Id.VideoId,
                    PublishedDate = s.Snippet.PublishedAt,
                    ChannelTitle = s.Snippet.ChannelTitle,
                    Description = description,
                    ChannelId = s.Snippet.ChannelId
                });
            }

            return list;
        }
    }
}
