using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler_Sample.Export
{
    public class ExportOptionStruct
    {
        #region [  Json  ]
        public string JsonFolderLocation { get; set; }

        public string JsonFileName { get; set; }

        public int JsonOverlapOption { get; set; }

        public bool JsonSort { get; set; }

        #endregion

        #region [  CSV  ]

        public string CSVFolderLocation { get; set; }

        public string CSVFileName { get; set; }

        public int CSVOverlapOption { get; set; }

        #endregion

        #region [  MySQL  ]

        public string MySQLConnAddr { get; set; }

        public string MySQLUserID { get; set; }

        public string MySQLUserPassword { get; set; }

        public string MySQLDatabaseName { get; set; }

        public string MySQLConnString { get; set; }

        public bool MySQLManualInput { get; set; }

        #endregion
    }
}
    