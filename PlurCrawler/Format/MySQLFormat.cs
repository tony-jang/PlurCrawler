using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;

using PlurCrawler.Format.Base;
using PlurCrawler.Search.Base;
using PlurCrawler.Attributes;
using PlurCrawler.Search.Services.GoogleCSE;
using PlurCrawler.Search.Services.Twitter;
using PlurCrawler.Search.Services.Youtube;
using PlurCrawler.Extension;

using MySql.Data.MySqlClient;

namespace PlurCrawler.Format
{
    /// <summary>
    /// MySQL 타입으로 내보내는 포맷을 나타냅니다.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class MySQLFormat<TResult> : BaseFormat<TResult>, IDisposable where TResult : ISearchResult
    {
        /// <summary>
        /// 지정된 포맷으로 <see cref="MySQLFormat{TResult}"/>을 초기화합니다.
        /// </summary>
        /// <param name="server">서버 이름 또는 주소입니다.</param>
        /// <param name="userId">사용자 ID입니다.</param>
        /// <param name="password">사용자 비밀번호입니다.</param>
        /// <param name="databaseName">데이터베이스 이름입니다.</param>
        public MySQLFormat(string server, string userId, string password, string databaseName)
            : this($"server={server};userid={userId};password={password};database={databaseName};Charset=euckr;")
        {
            this.Server = server;
            this.UserId = userId;
            this.Password = password;
            this.DataBaseName = databaseName;
        }

        /// <summary>
        /// 연결 문자열로 <see cref="MySQLFormat{TResult}"/>을 초기화합니다.
        /// </summary>
        /// <param name="connStr">DB 연결 문자열입니다.</param>
        public MySQLFormat(string connStr)
        {
            Connection = new MySqlConnection(connStr);
        }

        #region [  Property  ]

        /// <summary>
        /// 서버 주소 또는 이름입니다.
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// 사용자 ID입니다.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 사용자 비밀번호입니다.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 데이터베이스 이름입니다.
        /// </summary>
        public string DataBaseName { get; set; }

        /// <summary>
        /// 테이블 이름을 가져옵니다. 테이블 이름은 FormattingData 이전까지는 null을 유지합니다.
        /// </summary>
        public string TableName { get; internal set; }
        
        private MySqlConnection Connection { get; set; }

        /// <summary>
        /// MySQL이 현재 열린 상태인지를 나타냅니다.
        /// </summary>
        public ConnectionState? IsOpened => Connection?.State;

        #endregion

        #region [  Table Create  ]

        private void CreateTable()
        {

            if (!TableExists())
            {
                try
                {
                    string query = CreateTableQuery;
                    using (MySqlCommand cmd = new MySqlCommand(query, Connection))
                        cmd.ExecuteNonQuery();
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

                string prim = props.Where(i => i.IsPrimary).First().Name;

                foreach (TableField prop in props)
                {
                    sql.Append($"{prop.Name} {prop.Type}");
                    if (prop.IsPrimary)
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
                using (MySqlCommand cmd = new MySqlCommand(TableCheckQuery, Connection))
                {
                    using (MySqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int count = reader.GetInt32(0);
                            return count == 1;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
            return false;
        }

        #endregion

        #region [  Manage Properties  ]

        private IEnumerable<TableField> GetProperties()
        {
            string primaryProp;
            IEnumerable<PropertyInfo> properties = type.GetProperties().Where(i => i.GetCustomAttributes<IgnorePropertyAttribute>().Count() == 0);

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

            return properties.Select(i => new TableField(i.Name, GetTypeString(i), i.Name == primaryProp));
        }
        
        private string GetUpdateQuery(TableField field, object value, string keyword)
        {
            if (field.Type.StartsWith("VARCHAR") ||
                field.Type.StartsWith("LONGTEXT"))
            {
                return $"update {TableName} set keyword = CONCAT(keyword, ', ', '{keyword}') WHERE {field.Name} = '{value}';";
            }
            else if (field.Type.StartsWith("BIGINT") ||
                     field.Type.StartsWith("INT"))
            {
                return $"update {TableName} set keyword = CONCAT(keyword, ', ', '{keyword}') WHERE {field.Name} = {value};";
            }

            throw new Exception(field.Type + "타입에 대한 GetUpdateQuery 함수를 완성해야 합니다.");
        }

        Type type;

        private PropertyInfo GetPrimaryProperty()
        {
            return type.GetProperties().Where(i => i.GetCustomAttributes<PrimaryKeyAttribute>().Count() == 1).First();
        }

        private TableField ToTableField(PropertyInfo info)
        {
            return new TableField()
            {
                IsPrimary = info.GetCustomAttributes<PrimaryKeyAttribute>().Count() == 1,
                Name = info.Name,
                Type = GetTypeString(info)
            };
        }

        private IEnumerable<PropertyInfo> GetProperties(bool exceptIgnoreProperty)
        {
            if (exceptIgnoreProperty)
                return type.GetProperties().Where(i => i.GetCustomAttributes<IgnorePropertyAttribute>().Count() == 0);
            else
                return type.GetProperties();
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
                return "VARCHAR(200)";
            }
        }

        #endregion

        /// <summary>
        /// 연결을 해제합니다.
        /// </summary>
        public void Dispose()
        {
            Connection?.Dispose();
        }

        /// <summary>
        /// 바뀐 속성을 기반으로 연결을 다시 진행합니다.
        /// </summary>
        public void RenewConnection()
        {
            RenewConnection($"server={Server};userid={UserId};password={Password};database={DataBaseName};Charset=euckr;");
        }

        /// <summary>
        /// 지정된 연결 문자열로 연결을 다시 진행합니다..
        /// </summary>
        /// <param name="connStr"></param>
        public void RenewConnection(string connStr)
        {
            Connection = new MySqlConnection(connStr);
        }

        private void SetType(Type t)
        {
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
        }
        
        private void ExecuteCommand(TResult data, string baseQuery)
        {
            using (MySqlCommand cm = Connection.CreateCommand())
            {
                cm.CommandText = baseQuery;

                foreach (PropertyInfo prop in GetProperties(true))
                {
                    // Default Type
                    if (prop.PropertyType == typeof(bool))
                    {
                        cm.Parameters.AddWithValue($"@{prop.Name}", (bool)prop.GetValue(data) ? "True" : "False");
                    }
                    else
                    {
                        cm.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(data));
                    }
                }

                cm.ExecuteNonQuery();
            }
        }

        #region [  Check Keyword Exists  ]

        private bool KeywordExists(TableField primaryProp, object value, string keyword)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"select keyword from {TableName} where {primaryProp.Name} = ");

            if (primaryProp.Type.StartsWith("VARCHAR") ||
                primaryProp.Type.StartsWith("LONGTEXT"))
            {
                sb.Append($"'{value}'");
            }
            else if (primaryProp.Type.StartsWith("BIGINT") ||
                primaryProp.Type.StartsWith("INT"))
            {
                sb.Append($"{value}");
            }

            bool flag = false;

            using (MySqlCommand cm = Connection.CreateCommand())
            {
                cm.CommandText = sb.ToString();

                using (MySqlDataReader reader = cm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string findKeyword = reader["keyword"].ToString();

                        string[] keywords = findKeyword.Split(", ");

                        if (keywords.Contains(keyword))
                        {
                            flag = true;
                            break;
                        }
                    }
                }
            }

            return flag;
        }

        #endregion

        public override void FormattingData(IEnumerable<TResult> resultData)
        {
            if (resultData.Count() == 0)
                return;

            SetType(resultData.First().GetType());
            
            if (IsOpened != ConnectionState.Open)
                Connection.Open();

            CreateTable();
            
            string baseQuery = $"INSERT INTO {TableName} values({string.Join(", ", GetProperties().Select(i => $"@{i.Name}"))})";
            
            foreach (TResult data in resultData)
            {
                try
                {
                    ExecuteCommand(data, baseQuery);
                }
                catch (Exception ex)
                {
                    // 중복 오류가 발생했을시,
                    if (ex.HResult == -2147467259)
                    {
                        var primaryProp = GetPrimaryProperty();

                        // 키워드가 중복되지 않았을시 추가
                        if (!KeywordExists(ToTableField(primaryProp), primaryProp.GetValue(data), data.Keyword))
                        {
                            using (MySqlCommand cm = Connection.CreateCommand())
                            {
                                cm.CommandText = GetUpdateQuery(ToTableField(primaryProp), primaryProp.GetValue(data), data.Keyword);
                                cm.ExecuteNonQuery();
                            }
                        }
                        continue;
                    }

                    throw ex;
                }
            }
        }
    }
}
