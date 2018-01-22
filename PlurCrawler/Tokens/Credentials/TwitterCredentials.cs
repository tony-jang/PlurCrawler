namespace PlurCrawler.Tokens.Credentials
{
    /// <summary>
    /// Twitter API의 자격 정보를 저장하고 있는 클래스입니다.
    /// </summary>
    public class TwitterCredentials : ICredentials
    {
        /// <summary>
        /// <see cref="TwitterCredentials"/> 클래스를 컨슈머 키와 시크릿으로 초기화합니다.
        /// </summary>
        /// <param name="consumerKey"></param>
        /// <param name="consumerSecret"></param>
        public TwitterCredentials(string consumerKey, string consumerSecret)
        {
            this.ConsumerKey = consumerKey;
            this.ConsumerSecret = consumerSecret;
        }

        /// <summary>
        /// 컨슈머 키 입니다.
        /// </summary>
        public string ConsumerKey { get; set; }

        /// <summary>
        /// 컨슈머 비밀 키 입니다.
        /// </summary>
        public string ConsumerSecret { get; set; }

        /// <summary>
        /// 인증용 핀 번호를 나타냅니다.
        /// </summary>
        public string PINNumber { get; private set; }

        /// <summary>
        /// PIN 번호를 입력합니다.
        /// </summary>
        /// <param name="pinNumber">핀 번호입니다.</param>
        public void InputPIN(string pinNumber)
        {
            this.PINNumber = pinNumber;
        }

        /// <summary>
        /// PIN 번호를 입력합니다.
        /// </summary>
        /// <param name="pinNumber">핀 번호입니다.</param>
        public void InputPIN(int pinNumber)
        {
            InputPIN(pinNumber.ToString());
        }
    }
}
