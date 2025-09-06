using DAL.Entities.section;
using DAL.Enum.fileType;
using Microsoft.DotNet.Scaffolding.Shared;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL.Entities.slide
{
    public class Slide
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="The File Path is Required.")]
        public string FilePath { get; set; }
        [Required(ErrorMessage ="The File Type is Required.")]
        public FileType Type { get; set; }
        [Required(ErrorMessage ="The Section Id is Required.")]
        [ForeignKey("Section")]
        public int SectionId { get; set; }
        public Section Section { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
       

    }
}