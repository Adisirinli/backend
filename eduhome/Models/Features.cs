namespace BackEndProject_Edu.Models
{
    public class Features : BaseEntity
    {
        public DateTime StartDate { get; set; }
        public string Duration { get; set; }
        public string Hours { get; set; }
        public string Skill { get; set; }
        public string Language { get; set; }
        public int StuNum { get; set; }
        public string Assesment { get; set; }
        public ICollection<CourseFeature> courseFeatures { get; set; }

    }
}
