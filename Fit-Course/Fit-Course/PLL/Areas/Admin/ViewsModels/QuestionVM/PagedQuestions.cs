using BLL.DTOS.QuestionDTOs;


namespace PLL.Areas.Admin.ViewsModels.QuestionVM
{
    public class PagedQuestions
    {
        public List<QuestionDTO> Questions { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
