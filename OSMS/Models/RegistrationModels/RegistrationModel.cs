using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OSMS.Models.RegistrationModels
{
    public class RegistrationModel
    {
        [Required]
        public string StudentID { get; set; }
        [Required]
        public string StudentName { get; set; }
        [Required]
        public int StandardID { get; set; }
        [Required]
        public string Password { get; set; }
        [NotMapped]
        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        public IEnumerable<SelectListItem> StandardList { get; set; }
    }
}
