using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Quic;
using DAL.Entities.coachProgress;
using DAL.Entities.course;
using DAL.Entities.quiz;
using DAL.Entities.slide;
using DAL.Entities.video;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DAL.Entities.section
{
    public class Section
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The Title is Required.")]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "The Order is Required.")]
        public int OrderIndex { get; set; }
        [Required(ErrorMessage = "The Course Id is Required.")]
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public List<Slide>? Slide { get; set; }
        public List<Video>? Video { get; set; }
        public List<Quiz>? Quiz { get; set; }

        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        public List<CoachProgress> Progresses { get; set; }
    }
}