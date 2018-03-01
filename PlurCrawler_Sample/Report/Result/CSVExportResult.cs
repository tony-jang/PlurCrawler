using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Attributes;

namespace PlurCrawler_Sample.Report.Result
{
    public enum CSVExportResult
    {
        [Description("외부로 내보내도록 설정되지 않음")]
        NotSet,
        [Description("실패 - 파일 이름이 Null 입니다.")]
        [Bool(false)]
        Fail_FileNameNull,
        [Description("실패 - 폴더 이름이 Null 입니다.")]
        [Bool(false)]
        Fail_FileDirectoryNull,
        [Description("실패 - 폴더가 존재하지 않습니다.")]
        [Bool(false)]
        Fail_FileDirectoryNotExists,
        [Description("실패 - 파일 엑세스 권한이 거부되었습니다.")]
        [Bool(false)]
        Fail_FileAccessDenied,
        [Description("실패 - 알 수 없는 오류가 발생했습니다.")]
        [Bool(false)]
        Unknown,
        [Description("성공")]
        [Bool(true)]
        Success,

    }
}
