using System.ComponentModel.DataAnnotations;

namespace OSMS.Models.LoginModels
{
    public class LoginModel
    {
        [Required]
        public string UserID { get; set; }
        [Required]
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
