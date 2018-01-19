using PlurCrawler.Tokens.Tokenizer.Base;

namespace PlurCrawler.Tokens.Tokenizer
{
    /// <summary>
    /// 네이버 API에 관한 정보를 저장하고 있는 클래스입니다.
    /// </summary>
    public class NaverToken : IToken
    {
        /// <summary>
        /// <see cref="NaverToken"/> 클래스를 초기화합니다.
        /// </summary>
        /// <param name="clientId">클라이언트 ID 입니다.</param>
        /// <param name="clientSecret">클라이언트 시크릿입니다.</param>
        public NaverToken(string clientId, string clientSecret)
        {
            this.ClientId = clientId;
            this.ClientSecret = clientSecret;
        }

        /// <summary>
        /// 클라이언트 ID 입니다.
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// 클라이언트 시크릿입니다.
        /// </summary>
        public string ClientSecret { get; }
    }
}
