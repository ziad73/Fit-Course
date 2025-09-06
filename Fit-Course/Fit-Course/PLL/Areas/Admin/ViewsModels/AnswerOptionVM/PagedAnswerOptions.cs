using BLL.DTOS.AnswerOptionDTOS;

namespace PLL.Areas.Admin.ViewsModels.AnswerOptionVM
{
    public class PagedAnswerOptions
    {
        public List<AnswerOptionDTO> AnswerOptions { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
