using DAL.Entities.question;
using DAL.Entities.section;
using DAL.Enum.fileType;
using Microsoft.DotNet.Scaffolding.Shared;

namespace DAL.Entities.quiz
{
    public class Quiz
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Instructions { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }

        public Question Question { get; set; }
    }
}