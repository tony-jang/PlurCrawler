namespace PlurCrawler.Tokens.Credentials
{
    /// <summary>
    /// Twitter API의 인증 정보를 저장하고 있는 클래스입니다.
    /// </summary>
    public class TwitterCredentials : ICredentials
    {
        /// <summary>
        /// 컨슈머 키 입니다.
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// 컨슈머 비밀 키 입니다.
        /// </summary>
        public string ConsumerSecret { get; set; }

        public TwitterCredentialsType TwitterCredentialsType { get; set; } = TwitterCredentialsType.PIN;
    }
}
