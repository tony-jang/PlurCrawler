using System;

namespace PlurCrawler.Tokens.Credentials
{
    /// <summary>
    /// 인증 정보의 타입이 올바르게 연결되지 않았을때 발생하는 예외입니다.
    /// </summary>
    public class CredentialsTypeException : Exception
    {
        public CredentialsTypeException() { }
        public CredentialsTypeException(string message) : base(message) { }
        public CredentialsTypeException(string message, Exception inner) : base(message, inner) { }
        protected CredentialsTypeException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}
