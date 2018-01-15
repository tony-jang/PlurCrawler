namespace PlurCrawler.Tokens.Credentials
{
    /// <summary>
    /// 트위터 API 인증 타입을 결정합니다.
    /// </summary>
    public enum TwitterCredentialsType
    {
        /// <summary>
        /// 핀 번호 기반으로 인증 받습니다.
        /// </summary>
        PIN,
        /// <summary>
        /// 리다이렉트 URL 기반으로 인증 받습니다.
        /// </summary>
        RedirectURL,
    }
}
