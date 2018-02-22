using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler_Sample.Report.Result
{
    public class ExportResultPack
    {
        public CSVExportResult CSVExportResult { get; set; } = CSVExportResult.NotSet;

        public JsonExportResult JsonExportResult { get; set; } = JsonExportResult.NotSet;

        public MySQLExportResult MySQLExportResult { get; set; } = MySQLExportResult.NotSet;

        public AccessExportResult AccessExportResult { get; set; } = AccessExportResult.NotSet;
    }
}
