using System.Net.Quic;
using DAL.Entities.course;
using DAL.Entities.quiz;
using DAL.Entities.slide;
using DAL.Entities.video;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DAL.Entities.section
{
    public class Section
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public int OrderIndex { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }

        public Slide Slide { get; set; }
        public Video Video { get; set; }
        public Quiz Quiz { get; set; }


    }
}