using System.ComponentModel.DataAnnotations;

namespace BLL.DTOS.AccountDTOS
{
    public class LoginDTO
    {
        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Required.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
    }
}