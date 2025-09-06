
using BLL.DTOS.SectionDTOS;

namespace PLL.Areas.Admin.ViewsModels.SectionVM
{
    public class PagedSections
    {
        public List<SectionDTO> Sections { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
