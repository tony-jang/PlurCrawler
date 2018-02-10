using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using PlurCrawler.Format.Base;
using PlurCrawler.Search.Base;

namespace PlurCrawler.Format
{
    public class JsonFormat<TResult> : BaseFormat<TResult, string> where TResult : ISearchResult
    {
        public override string FormattingData(IEnumerable<TResult> resultData)
        {
            string json = JsonConvert.SerializeObject(resultData);
            return JToken.Parse(json).ToString(Formatting.Indented);
        }
    }
}
