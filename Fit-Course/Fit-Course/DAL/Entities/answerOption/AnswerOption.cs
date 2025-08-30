using DAL.Entities.question;
using DAL.Entities.section;
using DAL.Enum.fileType;
using Microsoft.DotNet.Scaffolding.Shared;

namespace DAL.Entities.answerOption
{
    public class AnswerOption
    {
        public int Id { get; set; }
        public bool IsCorrect { get; set; }
        // public string OptionText { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }
    }
}