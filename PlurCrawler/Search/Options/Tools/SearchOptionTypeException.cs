using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Search.Options.Tools
{
    /// <summary>
    /// 검색 옵션 타입이 잘못되었을때 발생하는 예외입니다.
    /// </summary>
    [Serializable]
    public class SearchOptionTypeException : Exception
    {
        /// <summary>
        /// <see cref="SearchOptionTypeException"/> 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        public SearchOptionTypeException() { }
        /// <summary>
        /// 지정된 오류 메세지를 사용하여 <see cref="SearchOptionTypeException"/> 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="message">예외 이유를 설명하는 오류 메세지입니다.</param>
        public SearchOptionTypeException(string message) : base(message) { }
        /// <summary>
        /// 지정된 오류 메세지와 해당 예외의 원인인 내부 예외에 대한 참조를 사용하여 <see cref="SearchOptionTypeException"/> 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="message">예외 이유를 설명하는 오류 메세지입니다.</param>
        /// <param name="inner">현재 예외의 원인인 예외입니다. inner 매개 변수가 null이 아니면 현재 예외는 내부 예외를 처리하는 catch 블록에서 발생합니다.</param>
        public SearchOptionTypeException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// serialize된 데이터를 사용하여 <see cref="SearchOptionTypeException"/> 클래스의 새 인스턴스를 초기화합니다.
        /// </summary>
        /// <param name="info">throw되는 예외에 대해 serialize된 개체 데이터를 보유하는 System.Runtime.Serialization.SerializationInfo 개체입니다.</param>
        /// <param name="context">소스 또는 대상에 대한 컨텍스트 정보를 포함하는 System.Runtime.Serialization.StreamingContext입니다.</param>
        protected SearchOptionTypeException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
