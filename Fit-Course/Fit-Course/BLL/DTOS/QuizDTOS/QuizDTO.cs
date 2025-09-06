using DAL.Entities.question;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOS.QuizDTOS
{
    public class QuizDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Title is Required.")]
        public string Title { get; set; }
     
        [Required(ErrorMessage = "The Max Mark is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "The Max Mark must be more than or equal 1.")]
        public double MaxMark { get; set; }
        [Required(ErrorMessage = "The Min Mark is Required.")]
        [Range(1, double.MaxValue, ErrorMessage = "The Min Mark must be more than or equal 1.")]

        public double MinMark { get; set; }
        [Required(ErrorMessage = "The Section Id is Required.")]
       
        public int SectionId { get; set; }
    }
}
