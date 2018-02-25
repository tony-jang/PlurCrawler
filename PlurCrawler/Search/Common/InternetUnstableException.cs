using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Common
{
    public class InternetUnstableException : Exception
    {
        public InternetUnstableException()
        {
        }

        public InternetUnstableException(string message) : base(message)
        {
        }

        public InternetUnstableException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InternetUnstableException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
