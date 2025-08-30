using System.ComponentModel.DataAnnotations;
using DAL.Entities.course;
using DAL.Enum.user;
using Humanizer;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities.user
{
    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public UserRole role { get; set; }
        public DateTime DateCreated { get; set; }

        public UserGender? Gender { get; set; }

        public List<Course>? Courses { get; set; }//instructor has * Courses 


    }
}