namespace PlurCrawler.Tokens.Tokenizer
{
    /// <summary>
    /// 트위터 API의 토큰을 저장하고 있습니다.
    /// </summary>
    public class TwitterToken : IToken
    {
        /// <summary>
        /// 트위터 API 토큰 클래스를 초기화합니다.
        /// </summary>
        /// <param name="token">트위터 API의 토큰입니다.</param>
        public TwitterToken(string token)
        {
            this.Token = token;
        }
        /// <summary>
        /// 트위터 API의 토큰입니다.
        /// </summary>
        public string Token { get; }
    }
}
