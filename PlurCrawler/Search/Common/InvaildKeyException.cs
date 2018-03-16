using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Common
{
    public class InvaildKeyException : Exception
    {
        public InvaildKeyException()
        {
        }

        public InvaildKeyException(string message) : base(message)
        {
        }

        public InvaildKeyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvaildKeyException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
