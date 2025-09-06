using DAL.Enum.fileType;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOS.SlideDTOS
{
    public class SlideDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Section Id is Required.")]

        public int SectionId { get; set; }
        [Required(ErrorMessage = "The Title is Required.")]

        public string Title { get; set; }
        [Required(ErrorMessage = "The Slide Path is Required.")]

        public IFormFile SlideUrl { get; set; }
        public FileType? Type { get; set; }
        public string? FilePath { get; set; }

    }
}
