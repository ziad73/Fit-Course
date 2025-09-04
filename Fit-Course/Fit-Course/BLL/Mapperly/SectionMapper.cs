using BLL.DTOS.SectionDTOS;
using DAL.Entities.section;

using Riok.Mapperly.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Mapperly
{
    [Mapper]
    public partial class SectionMapper
    {
        public partial SectionDTO MapToSectionDTO(Section Section);
        public partial Section MapToSection(SectionDTO Section);
        public partial List<SectionDTO> MapToSectionDTOList(List<Section> Sectiones);
    }
}
