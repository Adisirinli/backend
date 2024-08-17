using System.ComponentModel.DataAnnotations;

namespace BackEndProject_Edu.Areas.AdminArea.ViewModels.CourseVMs
{
    public class CourseCreateVM
    {
        [Required]
        public IFormFile ImgUrl { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Desc { get; set; }
        [Required]
        public string About { get; set; }
        [Required]
        public string Apply { get; set; }
        [Required]
        public string Certification { get; set; }

        public DateTime StartDate { get; set; }
        [Required]

        public string Duration { get; set; }
        [Required]

        public string Hours { get; set; }
        [Required]

        public string Skill { get; set; }
        [Required]

        public string Language { get; set; }
        [Required]
        [Range(20, 40, ErrorMessage = "The number of students must be between 20 and 40.")]
        public int StuNum { get; set; }
        [Required]

        public string Assesment { get; set; }
        [Required]

        public int? Price { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public List<int> TagIds { get; set; }

        //public List<int> FeaturesIds { get; set; }

        //public DateTime? Date { get; set; }
    }
}
