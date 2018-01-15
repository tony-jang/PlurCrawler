using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Tokens.Credentials;

namespace PlurCrawler.Tokens
{
    /// <summary>
    /// 토큰을 생성하는 기본 클래스입니다.
    /// </summary>
    abstract class BaseTokenizer
    {
        /// <summary>
        /// 인증 과정 중에 리다이렉션 URL을 가져오는 인증 URL을 반환합니다.
        /// </summary>
        /// <param name="credentials">인증을 위한 값입니다.</param>
        /// <returns></returns>
        /// <remarks>나중에 버전이 올라가서 지원하는 서비스가 많아졌을때에
        /// 따로 반환하는 클래스를 만들 가능성도 존재</remarks>
        public abstract string CredentialsRedirectCertification(ICredentials credentials);

        /// <summary>
        /// 인증 과정 중에 PIN 입력용 URL을 가져오는 인증 URL을 반환합니다.
        /// </summary>
        /// <param name="credentials">인증을 위한 값입니다.</param>
        /// <returns></returns>
        public abstract string CredentialsPINCertification(ICredentials credentials);

    }
}
