using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace DataAccess
{
    public sealed class SqlHelper
    {
        //************************************************************************
        //Variables:
        private static string _connectionString;
        public static object lockKey = new object();
        public static Dictionary<string, SqlParameter[]> SqlHelperParameterCache = new Dictionary<string, SqlParameter[]>();
        public static string ConnectionString
        {
            get { return _connectionString; }
            set { _connectionString = value; }
        }
        //************************************************************************
        private SqlHelper()
        {
        }
        //************************************************************************
        public static T ExcuteSP<T>(string spName, DataReaderDelegate<T> dataReaderPopulation, params object[] parameterValues) where T : new()
        {
            try
            {
                SqlParameter[] sqlParameters = null;
                if (parameterValues.Length > 0)
                {
                    sqlParameters = GetSPParameters(spName);
                    AssignSqlParametersValues(sqlParameters, parameterValues);
                }
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    T result = new T();
                    conn.Open();

                    SqlCommand cmd = new SqlCommand(spName, conn);

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Clear();
                    if (sqlParameters != null)
                        cmd.Parameters.AddRange(sqlParameters);

                    using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                    {
                        if (result is IList)
                        {
                            result = dataReaderPopulation(sqlDataReader);
                        }
                        else if (sqlDataReader.Read())
                        {
                            result = dataReaderPopulation(sqlDataReader);
                        }
                        return result;
                    }
                }
            }
            catch (Exception e)
            {
                //TODO: System event log
                throw e;
            }
        }
        //************************************************************************
        private static SqlParameter[] GetSPParameters(string spName)
        {
            if (!SqlHelperParameterCache.ContainsKey(spName))
            {
                SqlParameter[] sqlParameters;
                using (SqlConnection conn = new SqlConnection(ConnectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(spName, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlCommandBuilder.DeriveParameters(cmd);
                    cmd.Parameters.RemoveAt(0);
                    sqlParameters = new SqlParameter[cmd.Parameters.Count];
                    for (int i = 0; i < cmd.Parameters.Count; i++)
                    {
                        sqlParameters[i] = cmd.Parameters[i];
                    }
                }
                SetSqlHelperParameterCache(spName, sqlParameters);
            }
            SqlParameter[] sqlParam = CloneSqlParameters(SqlHelperParameterCache[spName]);

            return sqlParam;
        }
        //************************************************************************
        private static void SetSqlHelperParameterCache(string spName, SqlParameter[] sqlParameters)
        {
            lock (lockKey)
            {
                SqlHelperParameterCache.Add(spName, sqlParameters);
            }
        }

        //************************************************************************
        public static void AssignSqlParametersValues(SqlParameter[] sqlParameters, object[] values)
        {
            for (int index = 0; index < sqlParameters.Length; index++)
            {
                if (sqlParameters[index] is IDbDataParameter)
                {
                    sqlParameters[index].Value = values[index];
                }
            }
        }
        //************************************************************************
        private static SqlParameter[] CloneSqlParameters(SqlParameter[] inputSqlParameters)
        {
            SqlParameter[] sqlParameters = new SqlParameter[inputSqlParameters.Length];
            for (int index = 0; index < sqlParameters.Length; index++)
            {
                sqlParameters[index] = (SqlParameter)((ICloneable)inputSqlParameters[index]).Clone();
            }
            return sqlParameters;
        }

        //************************************************************************
        public static void ExecuteNoteQuery(string query)
        {
            ExecuteNoteQuery(ConnectionString, query);
        }
        //************************************************************************
        public static void ExecuteNoteQuery(string connectionString, string query)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {

                    //Regex regex = new Regex(@"\r{0,1}\nGO\r{0,1}\n");
                    string[] commands = query.Split(new[]
                    {
                        "GO", "Go", "go", "gO"
                    }, StringSplitOptions.RemoveEmptyEntries);
                    conn.Open();
                    for (int i = 0; i < commands.Length; i++)
                    {
                        if (commands[i] != string.Empty)
                        {
                            using (SqlCommand command = new SqlCommand(commands[i], conn))
                            {
                                command.ExecuteNonQuery();
                                command.Dispose();
                            }
                        }
                    }
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                //TODO: System event log
                throw ex;
            }
        }

        //************************************************************************
    }
}
