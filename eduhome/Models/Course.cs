namespace BackEndProject_Edu.Models
{
    public class Course : BaseEntity
    {
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }

        public string About { get; set; }
        public string Apply { get; set; }
        public string Certification { get; set; }
        public DateTime? Date { get; set; }

        public decimal? Price { get; set; }

        public ICollection<CourseFeature> CourseFeatures { get; set; }

        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<CourseTag> CourseTags { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<BasketCourse> BasketCourses { get; set; }

    }
}
