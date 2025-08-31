using DAL.Entities.section;
using DAL.Entities.user;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.coachProgress
{
    public class CoachProgress
    {
        [Key]
        public int Id { get; set; }
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletionDate { get; set; }
        [Required(ErrorMessage ="The Coach Id is Required.")]
        [ForeignKey("User")]
        public string CoachId { get; set; }
        public User User { get; set; }
        [Required(ErrorMessage ="The Section Id is Required.")]
        [ForeignKey("Section")]
        public int SectionId { get; set; }
        public Section Section { get; set; }
     
    }
}