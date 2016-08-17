using System.ComponentModel.DataAnnotations;

namespace SSOSimulator.Models
{
    public class LoginViewModel
    {
        [Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Licensee")]
        public string LicenseeId { get; set; }
    }
}