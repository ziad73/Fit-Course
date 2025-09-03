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

    public partial class FitCourseDb : IdentityDbContext<User>
    {
        public FitCourseDb() { }

        public FitCourseDb(DbContextOptions<FitCourseDb> options) : base(options) { }
        public virtual DbSet<Payment> Payment { get; set; }
        public virtual DbSet<AnswerOption> AnswerOption { get; set; }
        public virtual DbSet<CoachProgress> CoachProgress { get; set; }
        public virtual DbSet<Course> Course { get; set; }
        public virtual DbSet<Enrollment> Enrollment { get; set; }
        public virtual DbSet<Question> Question { get; set; }
        public virtual DbSet<Quiz> Quiz { get; set; }
        public virtual DbSet<QuizAttempt> QuizAttempt { get; set; }
        public virtual DbSet<Section> Section { get; set; }
        public virtual DbSet<Slide> Slide { get; set; }
        public virtual DbSet<Video> Video { get; set; }
        public virtual DbSet<Instructor> Instructor { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}