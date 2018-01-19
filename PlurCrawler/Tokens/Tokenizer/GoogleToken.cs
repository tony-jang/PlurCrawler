using PlurCrawler.Tokens.Tokenizer.Base;

namespace PlurCrawler.Tokens.Tokenizer
{
    /// <summary>
    /// 구글 API에 관한 정보를 저장하고 있는 클래스입니다.
    /// </summary>
    public class GoogleToken : IToken
    {
        /// <summary>
        /// <see cref="GoogleToken"/> 클래스를 초기화합니다.
        /// </summary>
        /// <param name="key">키 값입니다.</param>
        /// <param name="engineID">엔진 ID입니다.</param>
        public GoogleToken(string key, string engineID)
        {
            this.Key = key;
            this.EngineID = engineID;
        }

        /// <summary>
        /// 구글 API의 키 값입니다.
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 구글 API의 엔진 ID 정보입니다.
        /// </summary>
        public string EngineID { get; set; }
    }
}
