namespace BackEndProject_Edu.Models
{
    public class Comment : BaseEntity
    {
        public string Message { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
