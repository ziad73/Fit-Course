using DAL.Entities;
using DAL.Entities.answerOption;
using DAL.Entities.coachProgress;
using DAL.Entities.course;
using DAL.Entities.enrollment;
using DAL.Entities.instructor;
using DAL.Entities.question;
using DAL.Entities.quiz;
using DAL.Entities.quizAttempt;
using DAL.Entities.section;
using DAL.Entities.slide;
using DAL.Entities.user;
using DAL.Entities.video;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DAL.Database
{

    public class FitCourseDb : IdentityDbContext<User>
    {
        public FitCourseDb() { }

        public FitCourseDb(DbContextOptions<FitCourseDb> options) : base(options) { }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<AnswerOption> AnswerOption { get; set; }
        public DbSet<CoachProgress> CoachProgress { get; set; }
        public DbSet<Course> Course { get; set; }
        public DbSet<Enrollment> Enrollment { get; set; }
        public DbSet<Question> Question { get; set; }
        public DbSet<Quiz> Quiz { get; set; }
        public DbSet<QuizAttempt> QuizAttempt { get; set; }
        public DbSet<Section> Section { get; set; }
        public DbSet<Slide> Slide { get; set; }
        public DbSet<Video> Video { get; set; }
        public DbSet<Instructor> Instructor { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}