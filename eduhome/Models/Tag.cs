namespace BackEndProject_Edu.Models
{
    public class Tag : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<CourseTag> CourseTag { get; set; }
    }
}
