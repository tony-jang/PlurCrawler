using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Options
{

    /// <summary>
    /// 검색 옵션 타입이 잘못되었을때 발생하는 예외입니다.
    /// </summary>
    [Serializable]
    public class SearchOptionTypeException : Exception
    {
        public SearchOptionTypeException() { }
        public SearchOptionTypeException(string message) : base(message) { }
        public SearchOptionTypeException(string message, Exception inner) : base(message, inner) { }
        protected SearchOptionTypeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
