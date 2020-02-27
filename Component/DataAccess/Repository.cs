using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using System.Threading.Tasks;
using Component.Model;
using Microsoft.Win32;

namespace Component
{
    public abstract class Repository
    {
        //****************************************************************************************
        //Variables:
        private static Repository _repository = null;
        //****************************************************************************************
        static Repository()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["MabnaDataBase"].ConnectionString;
                string dataAccessRepository = "DataAccess." + ConfigurationManager.AppSettings["DataAccessProvider"] + ",DataAccess";
                _repository = (Repository)Activator.CreateInstance(Type.GetType(dataAccessRepository), new object[] { connectionString }); ;
            }
            catch (Exception ex)
            {
                //TODO log
                Console.WriteLine(ex);
                throw ex;
            }
        }
        //****************************************************************************************
        public static Repository Instance()
        {
            return _repository;
        }
        //****************************************************************************************
        #region Definition
        public abstract User GetUserByIdAndUserName(int id, string userName);
        public abstract List<User> GetAllusers();
        #endregion
    }
}
