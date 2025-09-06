using System.ComponentModel.DataAnnotations;

namespace BLL.DTOS.AccountDTOS
{
    public class ForgotPasswordDTO
    {
        [Required(ErrorMessage = "Required. ")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }
    }
}
