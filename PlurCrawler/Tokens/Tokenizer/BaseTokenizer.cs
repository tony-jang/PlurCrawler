using PlurCrawler.Tokens.Credentials;

namespace PlurCrawler.Tokens.Tokenizer
{
    /// <summary>
    /// 토큰을 생성하는 기본 클래스입니다.
    /// </summary>
    public abstract class BaseTokenizer
    {
        /// <summary>
        /// 인증 과정을 거칩니다.
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public abstract IToken CredentialsCertification(ICredentials credentials);

    }
}
