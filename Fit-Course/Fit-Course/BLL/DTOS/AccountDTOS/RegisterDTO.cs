using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;


namespace BLL.DTOS.AccountDTOS
{
    public class RegisterDTO
    {
        [Display(Name = "First Name")]
        [Required(ErrorMessage = "Required. ")]
        [MaxLength(100)]
        public string FName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Required. ")]
        [MaxLength(100)]
        public string LName { get; set; }

        [Display(Name = "Email Address")]
        [Required(ErrorMessage = "Required. ")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Remote(action: "isUnique", controller: "Account", ErrorMessage = "Already used")]

        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Required. ")]
        [StringLength(40, MinimumLength = 6, ErrorMessage = "Weak Password")]
        [DataType(DataType.Password)]
        [Compare("ConfirmPassword", ErrorMessage = "Password & Confirmed Password not matched.")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "Required. ")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

    }
}