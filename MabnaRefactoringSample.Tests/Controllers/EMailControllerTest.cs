using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MabnaRefactoringSample;
using MabnaRefactoringSample.Controllers;
using MabnaRefactoringSample.Models;

namespace MabnaRefactoringSample.Tests.Controllers
{
    [TestClass]
    public class EMailControllerTest
    {
        [TestMethod]
        public async Task Index()
        {
            // Arrange
            EMailController controller = new EMailController();
            EmailIndexInputModel emailIndexInputModel=new EmailIndexInputModel();
            emailIndexInputModel.id = 1;
            emailIndexInputModel.UserName = "user_1";
            // Act
            ActionResult result = await controller.Index(emailIndexInputModel);

            // Assert
            Assert.IsTrue(((ViewResultBase)result).Model.Equals("Successful"));
        }

       
    }
}
