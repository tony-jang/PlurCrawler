using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler_Sample.Common
{
    /// <summary>
    /// 파일이 중복될때에 관리할 옵션들을 나타냅니다.
    /// </summary>
    public enum FileOverrideOption
    {
        /// <summary>
        /// 오류를 발생시킵니다.
        /// </summary>
        ThrowError,
        /// <summary>
        /// 파일을 덮어씌웁니다.
        /// </summary>
        Override,
        /// <summary>
        /// 내용을 이어서 저장합니다.
        /// </summary>
        AppendContent,
        /// <summary>
        /// 파일이름에 부가적인 숫자를 추가해서 저장합니다.
        /// </summary>
        AppendAdditionalNumber
    }
}
