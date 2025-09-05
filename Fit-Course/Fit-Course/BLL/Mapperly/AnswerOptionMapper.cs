using BLL.DTOS.AnswerOptionDTOS;
using DAL.Entities.answerOption;

using Riok.Mapperly.Abstractions;


namespace BLL.Mapperly
{
    [Mapper]
    public partial class AnswerOptionMapper
    {
        public partial AnswerOptionDTO MapToAnswerOptionDTO(AnswerOption AnswerOption);
        public partial AnswerOption MapToAnswerOption(AnswerOptionDTO AnswerOption);
        public partial List<AnswerOptionDTO> MapToAnswerOptionDTOList(List<AnswerOption> AnswerOptiones);
    }
}
