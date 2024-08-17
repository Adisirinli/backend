namespace BackEndProject_Edu.Models
{
    public class CourseFeature : BaseEntity
    {
        public int FeaturesId { get; set; }
        public Features Features { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }


    }
}
