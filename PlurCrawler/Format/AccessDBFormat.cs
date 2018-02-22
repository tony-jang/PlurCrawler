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

namespace PlurCrawler.Format
{
    public class AccessDBFormat<TResult> : BaseFormat<TResult> where TResult : ISearchResult
    {
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
                var attr = info.GetCustomAttribute<AccessTypeAttribute>();

                return attr.TypeString;
            }
            catch (Exception)
            {
                return "VARCHAR(500)";
            }
        }

        #endregion

        public string FileName { get; set; }

        public string TableName { get; set; }

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
            
            string connStr = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={FileName}";

            using (OleDbConnection conn = new OleDbConnection(connStr))
            {
                conn.Open();

                CreateTable(conn);

                string baseQuery = $"INSERT INTO {TableName} values({string.Join(", ", GetProperties().Select(i => $"@{i.Item1}"))})";

                string sql = "";
                OleDbCommand cmd = new OleDbCommand(sql, conn);
                cmd.ExecuteNonQuery();
                
                foreach(TResult data in resultData)
                {
                    OleDbCommand cm = conn.CreateCommand();

                    cm.CommandText = baseQuery;

                    foreach (PropertyInfo prop in type.GetProperties())
                    {
                        cm.Parameters.AddWithValue($"@{prop.Name}", prop.GetValue(data));
                    }

                    cm.ExecuteNonQuery();
                }
            }
        }
    }
}
