namespace BackEndProject_Edu.Models
{
    public class BasketCourse : BaseEntity
    {
        public int Quantity { get; set; }
        public int BasketId { get; set; }
        public Basket Basket { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}
