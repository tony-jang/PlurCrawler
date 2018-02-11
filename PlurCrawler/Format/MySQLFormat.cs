using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PlurCrawler.Format.Base;
using PlurCrawler.Search.Base;

using MySql.Data.MySqlClient;

namespace PlurCrawler.Format
{
    public class MySQLFormat<TResult> : BaseFormat<TResult>, IDisposable where TResult : ISearchResult
    {
        public MySQLFormat(string server, string userId, string password, string databaseName, string tableName)
            : this($"server={server};userid={userId};password={password};database={databaseName};Charset=utf8;")
        {
            this.Server = server;
            this.UserId = userId;
            this.Password = password;
            this.DataBaseName = databaseName;
        }
        
        public MySQLFormat(string connStr)
        {
            Connection = new MySqlConnection(connStr);
        }

        string TableCheckQuery => $@"SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{
            DataBaseName}' AND table_name = '{TableName}';";

        public string Server { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string DataBaseName { get; set; }

        public string TableName { get; set; }

        private MySqlConnection Connection { get; set; }

        public ConnectionState? IsOpened => Connection?.State;

        public void Dispose()
        {
            Connection?.Dispose();
        }

        public void RenewConnection()
        {
            RenewConnection($"server={Server};userid={UserId};password={Password};database={DataBaseName};Charset=utf8;");
        }
        public void RenewConnection(string connStr)
        {
            Connection = new MySqlConnection(connStr);
        }

        public override void FormattingData(IEnumerable<TResult> resultData)
        {
            if (IsOpened != ConnectionState.Open)
                Connection.Open();
            
        }
    }
}
