using DAL.Entities.section;
using DAL.Entities.user;

namespace DAL.Entities.coachProgress
{
    public class CoachProgress
    {
        public int Id { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime CompletionDate { get; set; }

        public string CoachId { get; set; }
        public User Coach { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}