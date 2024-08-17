using BackEndProject_Edu.Models;

namespace BackEndProject_Edu.Areas.AdminArea.ViewModels.CourseVMs
{
    public class CourseDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string ImgUrl { get; set; }
        public string About { get; set; }
        public string Apply { get; set; }
        public string Certification { get; set; }
        public DateTime? Date { get; set; }
        public string CategoryName { get; set; }
        public decimal Price { get; set; }

        public List<Tag> CourseTags { get; set; } = new List<Tag>();
        public List<Features> CourseFeatures { get; set; } = new List<Features>();
    }
}
