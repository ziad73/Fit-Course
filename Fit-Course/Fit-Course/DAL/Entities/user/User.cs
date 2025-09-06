using System.ComponentModel.DataAnnotations;
using System.Data;
using DAL.Entities.coachProgress;
using DAL.Entities.course;
using DAL.Entities.enrollment;
using DAL.Entities.instructor;
using DAL.Entities.quizAttempt;
using DAL.Enum.user;
using Humanizer;
using Microsoft.AspNetCore.Identity;

namespace DAL.Entities.user
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? BLockedOn { get; set; }
        public bool IsBLocked { get; set; } = false;
        public string? BlockedBy { get; set; }
        public Instructor? Instructor { get; set; } // Navigation property

        public string? ResetCode { get; set; }
        public DateTime? ResetCodeExpiry { get; set; }

        //M TO M
        public List<CoachProgress>? Progresses { get; set; }
        public List<QuizAttempt>? QuizAttempts { get; set; }
        public List<Enrollment>? Enrollments { get; set; }

        //Language
        //HeadLine
        //Bio
        //Social media links 
    }
}