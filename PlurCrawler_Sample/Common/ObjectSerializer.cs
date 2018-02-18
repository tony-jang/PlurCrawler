using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PlurCrawler.Extension;

namespace PlurCrawler_Sample.Common
{
    public class ObjectSerializer<T> where T : class
    {
        public string Serialize(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize(string str)
        {
            if (str.IsNullOrEmpty())
                return null;
            
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}
