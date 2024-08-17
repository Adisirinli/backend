namespace BackEndProject_Edu.Models
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public DateTime Time { get; set; }
        public string Venue { get; set; }
        public string Desc { get; set; }
        public ICollection<Speaker> Speakers { get; set; }
        //public ICollection<Course> Courses { get; set; }

    }
}
