using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PlurCrawler_Sample.Common
{
    public class ObjectSerializer<T>
    {
        public string Serialize(T obj)
        {
            var xmlSerializer = new XmlSerializer(obj.GetType());

            var stream = new MemoryStream();
            xmlSerializer.Serialize(stream, obj);

            return StreamToString(stream);
        }

        public T Deserialize(string str)
        {
            var xmlSerializer = new XmlSerializer(typeof(T));

            return (T)xmlSerializer.Deserialize(StringToStream(str));
        }

        public static string StreamToString(Stream stream)
        {
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        public static Stream StringToStream(string src)
        {
            byte[] byteArray = Encoding.UTF8.GetBytes(src);
            return new MemoryStream(byteArray);
        }
    }
}
