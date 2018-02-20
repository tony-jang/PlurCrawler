using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

using PlurCrawler.Format.Base;
using PlurCrawler.Search.Base;
using PlurCrawler.Attributes;
using PlurCrawler.Search.Services.GoogleCSE;
using PlurCrawler.Search.Services.Twitter;
using PlurCrawler.Search.Services.Youtube;

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

        public string GetCreateTableQuery()
        {
            Type t = typeof(TResult);

            string tableName = string.Empty;

            if (t == typeof(GoogleCSESearchResult))
            {
                tableName = "GoogleResult";
            }
            else if (t == typeof(TwitterSearchResult))
            {
                tableName = "Tweets";
            }
            else if (t == typeof(YoutubeSearchResult))
            {
                tableName = "YoutubeVideos";
            }

            StringBuilder sql = new StringBuilder();

            sql.AppendLine($"CREATE TABLE {tableName} (");

            var props = GetProperties();

            string prim = props.Where(i => i.Item3).First().Item1;

            foreach ((string, string, bool) prop in props)
            {
                sql.Append($"{prop.Item1} {prop.Item2}");
                if (prop.Item3)
                {
                    sql.Append(" NOT NULL");
                }
                sql.AppendLine(",");
            }

            sql.AppendLine($"primary key ({prim}));");
            
            return sql.ToString();
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

        public IEnumerable<(string, string, bool)> GetProperties()
        {
            string primaryProp;
            IEnumerable<PropertyInfo> properties = typeof(TResult).GetProperties();
            
            try
            {
                primaryProp = properties.Where(i => i.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Count() >= 1)
                    .First()
                    .Name;
            }
            catch (Exception)
            {
                // 예외처리: 아무런 Primary Key가 없을 경우 임시로 첫번째의 프로퍼티를 Primary Key로 사용
                primaryProp = properties.First().Name;
            }

            return properties.Select(i => (i.Name, GetTypeString(i), i.Name == primaryProp));
        }
        
        public string GetTypeString(PropertyInfo info)
        {
            try
            {
                var attr = info.GetCustomAttribute<MySQLTypeAttribute>();

                return attr.TypeString;
            }
            catch (Exception)
            {
                return "VARCHAR(100)";
            }
        }

            /*
            string sql = "CREATE TABLE `TWEETS` ("
+ "  `ID` varchar(45) NOT NULL,"
+ "  `USER_NAME` varchar(45) DEFAULT NULL,"
+ "  `CREATE_DATE` varchar(45) DEFAULT NULL,"
+ "  `LANG` varchar(5) DEFAULT NULL,"
+ "  `MESSAGE` text,"
+ "  `KEYWORD` text,"
+ "  PRIMARY KEY (`ID`)"
+ ");";
             */

        public override void FormattingData(IEnumerable<TResult> resultData)
        {
            if (IsOpened != ConnectionState.Open)
                Connection.Open();
            
            string baseQuery = $"INSERT INTO {TableName} values({string.Join(", ", GetProperties().Select(i => $"@{i.Item1}"))})";
            
            foreach (TResult data in resultData)
            {
                try
                {
                    MySqlCommand cm = Connection.CreateCommand();

                    cm.CommandText = baseQuery;

                    foreach (PropertyInfo prop in typeof(TResult).GetProperties())
                    {
                        cm.Parameters.AddWithValue($"@{prop}", prop.GetValue(data));
                    }

                    cm.ExecuteNonQuery();
                }
                catch (Exception)
                {
                }
            }
        }
    }
}
