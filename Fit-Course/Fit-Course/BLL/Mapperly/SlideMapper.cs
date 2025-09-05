using BLL.DTOS.SlideDTOS;
using DAL.Entities.slide;
using Riok.Mapperly.Abstractions;


namespace BLL.Mapperly
{
    [Mapper]
    public partial class SlideMapper
    {
        public partial SlideDTO MapToSlideDTO(Slide Slide);
        public partial Slide MapToSlide(SlideDTO Slide);
        public partial List<SlideDTO> MapToSlideDTOList(List<Slide> Slidees);
    }
}
