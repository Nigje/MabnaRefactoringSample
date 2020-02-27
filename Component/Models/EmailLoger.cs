using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Component.Interface;
using Component.Managers;

namespace Component.Models
{
    public class EmailLoger:ILogManager
    {
        public void Log(string message)
        {
            try
            {
                string template = DateTime.Now.ToString() + " sent email to " + message + "\n";
                string path = ConfigurationManager.AppSettings["EmailLogPath"];
                File.AppendAllText(path, template);
            }
            catch (Exception e)
            {
                throw e;
            }
            
        }
    }
}
