using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Component.Model;
using Component.Utility;

namespace DataAccess
{
    public delegate T DataReaderDelegate<T>(IDataReader sqlDataReader);
    public class SqlMaper
    {
        //***********************************************************************************
        //Variables:
        //***********************************************************************************
        public static User PopulateUser(IDataReader dr)
        {
            Dictionary<string, string> dic = dr.GetDictionary();
            User user = new User();
            if (IsValidColumn("Id", dic, dr))
                user.Id = dr["Id"].ToString().SafeInt(-1);
            if (IsValidColumn("UserName", dic, dr))
                user.UserName = dr["UserName"].ToString();
            if (IsValidColumn("Email", dic, dr))
                user.Email = dr["Email"].ToString();
            return user;
        }
        //***********************************************************************************
        public static List<User> PopulateUsers(IDataReader dr)
        {
            List<User> users = new List<User>();
            while (dr.Read())
            {
                users.Add(PopulateUser(dr));
            }
            return users;
        }
        //***********************************************************************************
        private static bool IsValidColumn(string columnName, Dictionary<string, string> Dic, IDataReader dataReader)
        {
            if (Dic.ContainsKey(columnName.ToLower()) && dataReader[columnName] != DBNull.Value)
                return true;
            return false;
        }
        //***********************************************************************************

    }
}
