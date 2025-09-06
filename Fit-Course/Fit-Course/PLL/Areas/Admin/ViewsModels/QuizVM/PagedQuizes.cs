using BLL.DTOS.QuizDTOS;

namespace PLL.Areas.Admin.ViewsModels.QuizVM
{
    public class PagedQuizes
    {
        public List<QuizDTO> Quizs { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
