using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler_Sample.Report.Result
{
    public class ExportResultPack
    {
        public CSVExportResult? CSVExportResult { get; set; } = null;

        public JsonExportResult? JsonExportResult { get; set; } = null;

        public MySQLExportResult? MySQLExportResult { get; set; } = null;

    }
}
