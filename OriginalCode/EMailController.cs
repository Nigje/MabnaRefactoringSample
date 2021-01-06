using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace MabnaInterviewTest.Controllers
{
    public class EMailController : Controller
    {
        const string logFile = "log.txt";

        // This method get userId and username 
        // After checking database for user
        // Sends email to him/her
        public ActionResult Index()
        {
            string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password = myPassword;";

            SqlConnection con = new SqlConnection(connectionString);

            con.Open();

            SqlCommand cmd = new SqlCommand("select * from tblUsers where id = " + int.Parse(Request.QueryString["id"]) + " or username = '" + Request.QueryString["username"] + "'" , con);
            SqlDataReader reader = cmd.ExecuteReader();

            reader.Read();

            string email = reader.GetString(2);

            emailer.Instance.send(email);

            FileStream fs = System.IO.File.OpenWrite("c:\\myapp\\" + logFile);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write(DateTime.Now.ToString() + " sent email to " + email);

            fs.Close();

            return View();
        }

        // The class sends email
        public class emailer
        {
            private static emailer instance = null;

            public static emailer Instance
            {
                get {
                    if (instance == null)
                    {
                        instance = new emailer();
                    }
                    return instance;
                }
            }

            internal void send(string email) {
                try {
                    // Suppose the code is implemented.
                }
                catch (Exception ex)
                {
                    Console.Write(ex.ToString());
                }
            }
        }
    }
}
