using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using PlurCrawler.Extension;
using PlurCrawler.Search.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using YoutubeRequest = Google.Apis.YouTube.v3.ChannelsResource.ListRequest;

namespace PlurCrawler.Search.Services.Youtube
{
    public class YoutubeChannelSearcher : BaseSearcher<YoutubeChannelSearchOption, YoutubeSearchResult>
    {
        public string ApiKey { get; set; }

        public void Vertification(string apiKey)
        {
            this.ApiKey = apiKey;
            IsVerification = true;
        }

        public override IEnumerable<YoutubeSearchResult> Search(YoutubeChannelSearchOption searchOption)
        {
            YoutubeRequest listRequest = BuildRequest(searchOption);
            var list = new List<YoutubeSearchResult>();

            int remain = searchOption.SearchCount;
            int count = 1;

            string nextToken = string.Empty;
            // TODO : Search 구현
            while (true)
            {
                if (!nextToken.IsNullOrEmpty())
                {
                    listRequest.PageToken = nextToken;
                }
                else
                {

                }
            }

            return null;
        }

        public YoutubeRequest BuildRequest(YoutubeChannelSearchOption searchOption)
        {
            YouTubeService youtube = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = ApiKey
            });

            YoutubeRequest listRequest = youtube.Channels.List("id");
            listRequest.Id = searchOption.ChannelID;

            return listRequest;
        }
    }
}
