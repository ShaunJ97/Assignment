using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Display(Name = "Employee First Name")]
        public string EmployeeFirstName { get; set; }

        [Required]
        [Display(Name = "Employee Last Name")]
        public string EmployeeLastName { get; set; }

        [Required]
        [Display(Name = "Employee Date of Birth")]
        public DateTime EmployeeDoB { get; set; }

        [Display(Name = "Employee Date Hired")]
        public DateTime EmployeeDateHired { get; set; }

    }
}
