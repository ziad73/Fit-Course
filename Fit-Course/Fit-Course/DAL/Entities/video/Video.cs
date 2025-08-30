using DAL.Entities.section;
using DAL.Enum.fileType;
using Microsoft.DotNet.Scaffolding.Shared;

namespace DAL.Entities.video
{
    public class Video
    {
        public int Id { get; set; }
        public string VideoPath { get; set; }
        public decimal Duration { get; set; }
        public string Title { get; set; }
        public DateTime UploadDate { get; set; }

        public int SectionId { get; set; }
        public Section Section { get; set; }
    }
}