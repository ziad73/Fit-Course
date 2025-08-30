using DAL.Entities.section;
using DAL.Enum.fileType;
using Microsoft.DotNet.Scaffolding.Shared;

namespace DAL.Entities.slide
{
    public class Slide
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public FileType Type { get; set; }
        public DateTime UploadDate { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}