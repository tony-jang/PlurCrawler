using PlurCrawler.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler_Sample.Report.Result
{
    public enum AccessExportResult
    {
        [Note("외부로 내보내도록 설정되지 않음")]
        NotSet,
        [Note("실패 - 파일 이름이 Null 입니다.")]
        [Bool(false)]
        Fail_FileNameNull,
        [Note("실패 - 폴더 이름이 Null 입니다.")]
        [Bool(false)]
        Fail_FileDirectoryNull,
        [Note("실패 - 폴더가 존재하지 않습니다.")]
        [Bool(false)]
        Fail_FileDirectoryNotExists,
        [Note("실패 - 파일 엑세스 권한이 거부되었습니다.")]
        [Bool(false)]
        Fail_FileAccessDenied,
        [Note("실패 - 알 수 없는 오류가 발생했습니다.")]
        [Bool(false)]
        Unknown,
        [Note("성공")]
        [Bool(true)]
        Success,
    }
}
