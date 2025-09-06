using System.ComponentModel.DataAnnotations;

namespace BLL.DTOS.AccountDTOS
{

    public class VerifyCodeDTO
    {
        public string Email { get; set; }

        [Required(ErrorMessage = "Required.")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Code must be 6-digit")]
        public string Code { get; set; }
    }

}