using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Component.Utility
{
    public static class Extentions
    {
        //***************************************************************************************
        //Variables:

        //***************************************************************************************
        /// <summary>
        /// Mirzaie: Return Column names as dictionary
        /// </summary>
        /// <param name="dataReader"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionary(this IDataReader dataReader)
        {
            DataTable dataTable = dataReader.GetSchemaTable();
            Dictionary<string, string> Dic = new Dictionary<string, string>();
            foreach (DataRow row in dataTable.Rows)
            {
                string key = row["ColumnName"].ToString().ToLower();
                if (!Dic.ContainsKey(key))
                {
                    Dic.Add(key, "true");
                }
                else
                {

                }
            }
            return Dic;
        }
        //***************************************************************************************
        public static int SafeInt(this string number,int defaultValue)
        {
            try
            {
                return Convert.ToInt32(number);
            }
            catch
            {
                return defaultValue;
            }
        }
        //***************************************************************************************
    }
}
