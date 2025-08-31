using System.ComponentModel.DataAnnotations;
using System.Data;
using DAL.Entities.course;
using DAL.Enum.user;
using Humanizer;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities.user
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage ="The Name is Required.")]
        public string FullName { get; set; }
        [Required(ErrorMessage ="The Gender is Required.")]
        public UserGender Gender { get; set; }

        public List<Course>? Courses { get; set; }//instructor has * Courses 
        public DateTime CreatedOn { get; set; }
        public DateTime? BLockedOn { get; set; }
        public bool IsBLocked { get; set; }=false;
        public string? BlockedBy { get; set; }
    }
}