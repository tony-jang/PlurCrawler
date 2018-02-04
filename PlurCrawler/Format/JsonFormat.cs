using Newtonsoft.Json;
using PlurCrawler.Format.Base;
using PlurCrawler.Search.Base;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Format
{
    public class JsonFormat<TResult> : BaseFormat<TResult, string> where TResult : ISearchResult
    {
        public override string Formatting(TResult resultData)
        {
            return JsonConvert.SerializeObject(resultData);
        }

        public bool SaveFile(string fileLocation, string data)
        {
            try
            {
                var sw = new StreamWriter(fileLocation, false);
                sw.Write(data);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
