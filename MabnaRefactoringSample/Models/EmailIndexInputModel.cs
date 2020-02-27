using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace MabnaRefactoringSample.Models
{
    //this is sample name
    public class EmailIndexInputModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string UserName { get; set; }
    }

}