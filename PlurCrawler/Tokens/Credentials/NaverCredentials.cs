namespace PlurCrawler.Tokens.Credentials
{
    /// <summary>
    /// 네이버 API의 자격 정보를 저장하고 있는 클래스입니다.
    /// </summary>
    public class NaverCredentials : ICredentials
    {
        /// <summary>
        /// 클라이언트 ID 입니다.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// 클라이언트 시크릿입니다.
        /// </summary>
        public string ClientSecret { get; set; }
    }
}
