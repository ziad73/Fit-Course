using DAL.Entities.quiz;
using DAL.Entities.qustionType;
using DAL.Entities.section;
using DAL.Enum.fileType;
using Microsoft.DotNet.Scaffolding.Shared;

namespace DAL.Entities.question
{
    public class Question
    {
        public int Id { get; set; }
        public QustionType Type { get; set; }
        public string QuestionText { get; set; }

        public int QuizId { get; set; }
        public Quiz Quiz { get; set; }
    }
}