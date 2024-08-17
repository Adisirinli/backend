using BackEndProject_Edu.Models;
using System.ComponentModel.DataAnnotations;

namespace BackEndProject_Edu.Areas.AdminArea.ViewModels.CourseVMs
{
    public class CourseUpdateVM
    {
        public int Id { get; set; }
        public string ImgUrl { get; set; }
        public IFormFile Photo { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string About { get; set; }
        public string Apply { get; set; }
        public string Certification { get; set; }
        public DateTime StartDate { get; set; }

        public string Duration { get; set; }

        public string Hours { get; set; }

        public string Skill { get; set; }

        public string Language { get; set; }
        [Range(20, 40, ErrorMessage = "The number of students must be between 20 and 40.")]
        public int StuNum { get; set; }

        public string Assesment { get; set; }

        public int? Price { get; set; }

        public int CategoryId { get; set; }

        public List<int> TagIds { get; set; }
        public IEnumerable<CourseTag> Tags { get; set; }


        public List<int> FeaturesIds { get; set; }
        public IEnumerable<CourseFeature> Features { get; set; }


        public DateTime? Date { get; set; }
    }
}
