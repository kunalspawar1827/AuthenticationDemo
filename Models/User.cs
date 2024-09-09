using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AuthenticationDemo.Models
{
    public class User
    {
        [Required(ErrorMessage ="Enter Password")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Enter Username")]
        public string UserName { get; set; }

    }
}