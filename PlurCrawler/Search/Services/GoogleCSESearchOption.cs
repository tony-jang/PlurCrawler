using PlurCrawler.Search.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Services.GoogleCSE
{
    public class GoogleCSESearchOption : ISearchOption
    {
        public string Query { get; set; }

        public ulong SearchCount { get; set; }

        public ulong Offset { get; set; }
    }
}