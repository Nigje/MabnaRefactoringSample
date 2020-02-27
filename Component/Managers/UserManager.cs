using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Component.Model;

namespace Component.Managers
{
    public class UserManager
    {
        //********************************************************************************
        //Variables:
        //********************************************************************************
        public User GetUserByIdAndUserName(int id, string userName)
        {
            return Repository.Instance().GetUserByIdAndUserName(id, userName);
        }
        //********************************************************************************
        public List<User> GetAllusers()
        {
            return Repository.Instance().GetAllusers();
        }
        //********************************************************************************
    }
}
