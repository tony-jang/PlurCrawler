using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

using PlurCrawler.Format.Base;
using PlurCrawler.Search.Base;
using System.Reflection;
using PlurCrawler.Search.Services.GoogleCSE;
using PlurCrawler.Search.Services.Twitter;
using PlurCrawler.Search.Services.Youtube;
using PlurCrawler.Attributes;
using System.IO;
using PlurCrawler.Extension;

namespace PlurCrawler.Format
{
    /// <summary>
    /// Access DB 타입으로 내보내는 포맷을 나타냅니다.
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class AccessDBFormat<TResult> : BaseFormat<TResult> where TResult : ISearchResult
    {
        /// <summary>
        /// 파일 위치로 <see cref="AccessDBFormat{TResult}"/>을 초기화합니다.
        /// </summary>
        /// <param name="fileName"></param>
        public AccessDBFormat(string fileName)
        {
            this.FileName = fileName;
            try
            {
                var cat = new ADOX.Catalog();
                cat.Create($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={fileName}");
            }
            catch (Exception ex)
            {
                bool knownException = false;
                if (ex.Message.ToLower().Contains("database already exists"))
                {
                    knownException = true;
                }

                if (!knownException)
                    throw ex;
            }
        }
        
        /// <summary>
        /// Access에 접근하는 Connector가 설치되었는지에 대한 여부를 가져옵니다.
        /// </summary>
        /// <returns></returns>
        public static bool AccessConnectorInstalled()
        {
            string path = Path.Combine(Path.GetTempPath(), "testfile____.accdb");

            try
            {
                var cat = new ADOX.Catalog();
                cat.Create($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={path}");
                cat.ActiveConnection.Close();
            }
            catch (Exception ex)
            {
                if (ex.HResult == -2147221164)
                {
                    return false;
                }
            }

            File.Delete(path);

            return true;
        }

        #region [  Table 생성 관리  ]

        private string CreateTableQuery()
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

        private void CreateTable(OleDbConnection connection)
        {
            try
            {
                string query = CreateTableQuery();
                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (OleDbException ex)
            {
                if (ex.ErrorCode == 3010 || ex.ErrorCode == 3012)
                {

                }
            }
        }

        #endregion

        #region [  Property 관리  ]

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
                var attr = info.GetCustomAttribute<AccessTypeAttribute>();

                return attr.TypeString;
            }
            catch (Exception)
            {
                return "VARCHAR";
            }
        }

        #endregion

        /// <summary>
        /// 내보낸 위치의 파일명을 가져옵니다.
        /// </summary>
        public string FileName { get; set; }

        public string TableName { get; set; }

        Type type;
        
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

        private void ExecuteCommand(OleDbConnection conn, TResult data, string baseQuery)
        {
            OleDbCommand cmd = new OleDbCommand(baseQuery, conn);

            foreach (PropertyInfo prop in GetProperties(true))
            {
                cmd.Parameters.AddWithValue($"@{prop.Name}", GetValue());

                object GetValue()
                {
                    object value = prop.GetValue(data);
                    if (value == null)
                        return DBNull.Value;

                    return value;
                }
            }

            cmd.ExecuteNonQuery();
        }
        
        public string GetUpdateQuery(TableField field, object value, string keyword)
        {
            if (field.Type.StartsWith("VARCHAR") ||
                field.Type.StartsWith("LONGTEXT"))
            {
                return $"update {TableName} set keyword = keyword & ', ' & '{keyword}' WHERE {field.Name} = '{value}';";
            }
            else if (field.Type.StartsWith("BIGINT") ||
                     field.Type.StartsWith("INT"))
            {
                return $"update {TableName} set keyword = keyword & ', ' & '{keyword}' WHERE {field.Name} = {value};";
            }

            throw new Exception(field.Type + "타입에 대한 GetUpdateQuery 함수를 완성해야 합니다.");
        }

        public bool KeywordExists(TableField primaryProp, object value, string keyword, OleDbConnection conn)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"select Keyword from {TableName} where {primaryProp.Name} = ");

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

            using (OleDbCommand cm = conn.CreateCommand())
            {
                cm.CommandText = sb.ToString();

                using (OleDbDataReader reader = cm.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string findKeyword = reader["Keyword"].ToString();

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
        
        private TableField ToTableField(PropertyInfo info)
        {
            return new TableField()
            {
                IsPrimary = info.GetCustomAttributes<PrimaryKeyAttribute>().Count() == 1,
                Name = info.Name,
                Type = GetTypeString(info)
            };
        }

        public override void FormattingData(IEnumerable<TResult> resultData)
        {
            if (resultData.Count() == 0)
                return;

            SetType(resultData.First().GetType());

            string connStr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={FileName}";

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();

                CreateTable(conn);

                string baseQuery = $"INSERT INTO {TableName} values({string.Join(", ", GetProperties().Select(i => $"@{i.Name}"))})";
                
                foreach(TResult data in resultData)
                {
                    try
                    {
                        ExecuteCommand(conn, data, baseQuery);
                    }
                    catch (Exception ex)
                    {
                        if (ex.HResult == -2147467259)
                        {
                            var primaryProp = type.GetProperties().Where(i => i.GetCustomAttributes<PrimaryKeyAttribute>().Count() == 1).First();

                            // 키워드가 중복되지 않았을시 추가
                            if (!KeywordExists(ToTableField(primaryProp), primaryProp.GetValue(data), data.Keyword, conn))
                            {
                                using (OleDbCommand cm = conn.CreateCommand())
                                {
                                    cm.CommandText = GetUpdateQuery(ToTableField(primaryProp), primaryProp.GetValue(data), data.Keyword);
                                    cm.ExecuteNonQuery();
                                }
                            }
                            continue;
                        }
                    }
                    
                }
            }
        }
    }
}
