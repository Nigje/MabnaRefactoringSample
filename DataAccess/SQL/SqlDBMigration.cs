using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.SQL
{
    public class SqlDBMigration
    {
        public static void MigrateDB()
        {
            try
            {
                string createDataBaseQuery= ConfigurationManager.AppSettings["CreateDataBaseQuery"];
                var assembly = Assembly.GetExecutingAssembly();
                IEnumerable<string> scriptNames = assembly.GetManifestResourceNames().Where(x => x.EndsWith(".sql")).OrderBy(x=>x);
                foreach (string file in scriptNames)
                {
                    string result = string.Empty;
                    using (Stream stream = assembly.GetManifestResourceStream(file))
                    {
                        using (StreamReader sr = new StreamReader(stream))
                        {
                            result = sr.ReadToEnd();
                        }
                    }
                    if (file.EndsWith(createDataBaseQuery))
                    {
                        string conString = SqlHelper.ConnectionString;
                        SqlConnectionStringBuilder sqlConnection=new SqlConnectionStringBuilder(conString);
                        sqlConnection["Initial Catalog"] = "master";
                        SqlHelper.ExecuteNoteQuery(sqlConnection.ConnectionString,result);
                    }
                    else
                    {
                        SqlHelper.ExecuteNoteQuery(result);
                    }
                }
            }
            catch (Exception e)
            {
                //Todo: log in system Event
                throw e;
            }

        }
    }
}
