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
        public MySQLFormat(string server, string userId, string password, string databaseName)
            : this($"server={server};userid={userId};password={password};database={databaseName};Charset=euckr;")
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

        private void CreateTable()
        {

            if (!TableExists())
            {
                try
                {
                    string query = CreateTableQuery;
                    using (MySqlCommand cmd = new MySqlCommand(query, Connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (MySqlException ex)
                {
                    throw ex;
                }
            }
        }

        private string CreateTableQuery
        {
            get
            {
                StringBuilder sql = new StringBuilder();

                sql.AppendLine($"CREATE TABLE {TableName} (");

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
        }

        string TableCheckQuery => $@"SELECT COUNT(*) FROM information_schema.tables WHERE table_schema = '{
            DataBaseName}' AND table_name = '{TableName}';";

        private bool TableExists()
        {
            if (IsOpened != ConnectionState.Open)
                Connection.Open();

            try
            {
                MySqlCommand cmd = new MySqlCommand(TableCheckQuery, Connection);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int count = reader.GetInt32(0);
                        return count == 1;
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        public string Server { get; set; }

        public string UserId { get; set; }

        public string Password { get; set; }

        public string DataBaseName { get; set; }

        public string TableName { get; internal set; }

        private MySqlConnection Connection { get; set; }

        public ConnectionState? IsOpened => Connection?.State;

        public void Dispose()
        {
            Connection?.Dispose();
        }

        public void RenewConnection()
        {
            RenewConnection($"server={Server};userid={UserId};password={Password};database={DataBaseName};Charset=euckr;");
        }
        public void RenewConnection(string connStr)
        {
            Connection = new MySqlConnection(connStr);
        }

        private IEnumerable<(string, string, bool)> GetProperties()
        {
            string primaryProp;
            IEnumerable<PropertyInfo> properties = type.GetProperties();
            
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

        private string GetTypeString(PropertyInfo info)
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

        Type type;

        public override void FormattingData(IEnumerable<TResult> resultData)
        {
            if (resultData.Count() == 0)
                return;

            Type t = resultData.First().GetType();
            if (t == typeof(GoogleCSESearchResult))
            {
                TableName = "GoogleResult";
                type = typeof(GoogleCSESearchResult);
            }
            else if (t == typeof(TwitterSearchResult))
            {
                TableName = "Tweets";
                type = typeof(TwitterSearchResult);
            }
            else if (t == typeof(YoutubeSearchResult))
            {
                TableName = "YoutubeVideos";
                type = typeof(YoutubeSearchResult);
            }
            
            if (IsOpened != ConnectionState.Open)
                Connection.Open();

            CreateTable();
            
            string baseQuery = $"INSERT INTO {TableName} values({string.Join(", ", GetProperties().Select(i => $"@{i.Item1}"))})";
            
            foreach (TResult data in resultData)
            {
                try
                {
                    MySqlCommand cm = Connection.CreateCommand();

                    cm.CommandText = baseQuery;

                    foreach (PropertyInfo prop in type.GetProperties())
                    {
                        cm.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(data));
                    }

                    cm.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
    }
}
