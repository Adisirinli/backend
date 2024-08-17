using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndProject_Edu.Models
{
    public class Blog : BaseEntity
    {
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public DateTime? Date { get; set; }

        [NotMapped]
        public string ShortDesc => Desc.Length > 100 ? Desc.Substring(0, 50) : Desc;

        //public ICollection<CourseBlog> CoursesBlogs { get; set; }
    }
}
