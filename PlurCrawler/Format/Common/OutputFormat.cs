using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlurCrawler.Format.Common
{
    public enum OutputFormat
    {
        None = 0,
        CSV = 1,
        Json = 2,
        MySQL = 4,
        MSSQL = 8,
    }
}
