using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Crafty.Models
{
    public class Authentication
    {
        [DisplayName("User Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Give your User Name")]
        public string UserName { get; set; }
        [DisplayName("Password")]
        [DataType(DataType.Password)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Give your Password")]

       
        public string Password { get; set; }
    }
}