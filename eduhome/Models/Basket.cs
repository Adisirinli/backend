namespace BackEndProject_Edu.Models
{
    public class Basket : BaseEntity
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public ICollection<BasketCourse> BasketCourses { get; set; }
    }
}
