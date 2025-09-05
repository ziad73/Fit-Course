using DAL.Enum.fileType;
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
        [Required(ErrorMessage = "The File Path is Required.")]
        public string FilePath { get; set; }
        [Required(ErrorMessage = "The Section Id is Required.")]

        public int SectionId { get; set; }

    }
}
