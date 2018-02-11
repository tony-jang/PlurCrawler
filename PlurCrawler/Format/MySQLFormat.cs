using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Format.Base;
using PlurCrawler.Search.Base;

using MySql.Data.MySqlClient;

namespace PlurCrawler.Format
{
    public class MySQLFormat<TResult> : BaseFormat<TResult> where TResult : ISearchResult
    {
        public void Initalization(string server, string userId, string password, string databaseName)
        {
            this.Server = server;
            this.UserId = userId;
            this.Password = password;
            this.DataBaseName = databaseName;
        }

        string Server { get; set; }

        string UserId { get; set; }

        string Password { get; set; }

        string DataBaseName { get; set; }

        public override void FormattingData(IEnumerable<TResult> resultData)
        {
            string connStr = $"server={Server};userid={UserId};password={Password};database={DataBaseName};Charset=utf8;";
            using (MySqlConnection conn = new MySqlConnection(connStr))
            {
                conn.Open();
            }
        }
    }
}
