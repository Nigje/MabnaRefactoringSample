using System;
using System.Collections.Generic;
using Component;
using Component.Model;
using DataAccess.SQL;

namespace DataAccess
{
    public class SqlRepository : Repository
    {
        //*****************************************************************************
        //Variables:
        //*****************************************************************************
        public SqlRepository(string connectionString)
        {
            SqlHelper.ConnectionString = connectionString;
            SqlDBMigration.MigrateDB();
        }
        //*****************************************************************************
        public override User GetUserByIdAndUserName(int id, string userName)
        {
            User user = SqlHelper.ExcuteSP<User>("GetUserByIdAndUserName", SqlMaper.PopulateUser,id,userName);
            return user;
        }
        //*****************************************************************************
        public override List<User> GetAllusers()
        {
            List<User> users = SqlHelper.ExcuteSP<List<User>>("GetAllUsers", SqlMaper.PopulateUsers);
            return users;
        }
        //*****************************************************************************

    }
}
