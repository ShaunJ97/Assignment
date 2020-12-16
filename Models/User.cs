using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class User
    {
        public int Id { get; set; }


        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Role")]
        public string Role { get; set; }

        //[Display(Name = "sessionID")]
        //public string SessionID { get; set; }

    }
}
