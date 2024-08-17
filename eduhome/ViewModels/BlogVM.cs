using BackEndProject_Edu.Models;

namespace BackEndProject_Edu.ViewModels
{
    public class BlogVM
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public IEnumerable<Course> Courses { get; set; }
        public string ImgURl { get; set; }
        public DateTime? Date { get; set; }
    }
}
