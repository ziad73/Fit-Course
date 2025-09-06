

using BLL.DTOS.SlideDTOS;

namespace PLL.Areas.Admin.ViewsModels.SlideVM
{
    public class PagedSlides
    {
        public List<SlideDTO> Slides { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
