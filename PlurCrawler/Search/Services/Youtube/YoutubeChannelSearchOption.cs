using PlurCrawler.Format.Common;
using PlurCrawler.Search.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Services.Youtube
{
    public class YoutubeChannelSearchOption : ISearchOption
    {
        public string ChannelID { get; set; }

        public int SearchCount { get; set; }

        // Offset Check

        public OutputFormat OutputServices { get; set; }
    }
}
