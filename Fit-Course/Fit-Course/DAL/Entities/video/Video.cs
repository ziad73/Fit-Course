using DAL.Entities.section;
using DAL.Enum.fileType;
using Microsoft.DotNet.Scaffolding.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.video
{
    public class Video
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="The Video Path is Required.")]
        public string VideoPath { get; set; }
        [Required(ErrorMessage ="The Duration is Required.")]
        [Range(1,double.MaxValue,ErrorMessage ="The duration must be more than or equal 1 minute.")]
        public double Duration { get; set; }// his unite is minute 
        [Required(ErrorMessage ="The Title is Required.")]
        public string Title { get; set; }
        [Required(ErrorMessage ="The Section Id is Requirde.")]
        [ForeignKey("Section")]
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}