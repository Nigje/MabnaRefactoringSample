using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Component;
using MabnaRefactoringSample.Models;

namespace MabnaRefactoringSample.Controllers
{
    public class EMailController : Controller
    {
        // GET: EMail
        public async Task<ActionResult> Index(EmailIndexInputModel emailIndexInputModel)
        {
            object obj=new object();
            if (ModelState.IsValid)
            {
                try
                {
                    EmailManager emailManager = new EmailManager();
                    await emailManager.SendEmailByIdAndUserName(emailIndexInputModel.id, emailIndexInputModel.UserName);
                    obj = "Successful";
                }
                catch (Exception e)
                {
                    //TODO: Set model message as "Please try later";
                    obj = "Failed";
                }
            }
            return View(obj);
        }
    }
}