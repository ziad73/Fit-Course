using DAL.Entities.quiz;
using DAL.Entities.section;
using DAL.Entities.user;
using DAL.Enum.fileType;
using Microsoft.DotNet.Scaffolding.Shared;

namespace DAL.Entities.quizAttempt
{
    public class QuizAttempt
    {
        public int Id { get; set; }
        public decimal Score { get; set; }
        public DateTime AttemptDate { get; set; }

        public string CoachId { get; set; }
        public User Coach { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}