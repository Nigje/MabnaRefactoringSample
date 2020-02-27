using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Component.Managers;
using Component.Model;
using Component.Models;

namespace Component
{
    public class EmailManager
    {
        //*********************************************************************************
        //Variables:
        private static EmailManager instance = null;
        //*********************************************************************************
        public static EmailManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new EmailManager();
                }
                return instance;
            }
        }
        //*********************************************************************************
        internal void send(string email)
        {
            try
            {
                // فرض کنید این تکه از کد پیاده سازی شده است
                LogManager logManager = new LogManager(new EmailLoger());
                logManager.Log(email);
            }
            catch (Exception ex)
            {
                //Todo: log in system Event
                throw ex;
            }
        }
        //*********************************************************************************
        public async Task SendEmailByIdAndUserName(int id, string userName)
        {
            await Task.Run(() =>
            {
                try
                {
                    User user = Repository.Instance().GetUserByIdAndUserName(id, userName);
                    Instance.send(user.Email);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }
        //*********************************************************************************

        //*********************************************************************************
    }
}
