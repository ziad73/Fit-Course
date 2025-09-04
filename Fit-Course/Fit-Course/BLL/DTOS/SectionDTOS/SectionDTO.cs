using DAL.Entities.course;
using DAL.Entities.quiz;
using DAL.Entities.slide;
using DAL.Entities.video;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOS.SectionDTOS
{
    public class SectionDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Title is Required.")]
        public string Title { get; set; }
        public string? Description { get; set; }
        [Required(ErrorMessage = "The Order is Required.")]
        public int OrderIndex { get; set; }
        [Required(ErrorMessage = "The Course Id is Required.")]
        public int CourseId { get; set; }
       
    }
}
