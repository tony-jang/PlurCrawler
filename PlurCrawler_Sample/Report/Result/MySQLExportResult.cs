using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Attributes;

namespace PlurCrawler_Sample.Report.Result
{
    public enum MySQLExportResult
    {
        [Note("실패 - 올바르지 않은 연결입니다.")]
        [Bool(false)]
        Fail_InvaildConnection,
        [Note("실패 - 알 수 없는 오류가 발생했습니다.")]
        [Bool(false)]
        Fail_UnkownError,
        [Note("외부로 내보내도록 설정되지 않음")]
        NotSet,
        [Note("성공")]
        [Bool(true)]
        Success
    }
}
