using DAL.Entities.user;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities.section;
using Microsoft.AspNetCore.Http;

namespace BLL.DTOS.CourseDTOS
{
    public class CourseDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [Required(ErrorMessage = "The Description is Required.")]
        public string Description { get; set; }
        public string? Status { get; set; }
        public User User { get; set; }
        [Required(ErrorMessage = "The Instructor Id is Required.")]
       
        public string InstructorId { get; set; }
        [Required(ErrorMessage = "The Course Price is Required.")]
        [Range(0, double.MaxValue, ErrorMessage = "The Price muset be more than or equal 0")]
        public double Price { get; set; }
        public string? ImagePath { get; set; } 
        public IFormFile? ImageUrl { get; set; }
    }
}
