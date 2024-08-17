using BackEndProject_Edu.Models;

namespace BackEndProject_Edu.Areas.AdminArea.ViewModels.EventVMs
{
    public class EventDetailVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public DateTime Time { get; set; }
        public string Venue { get; set; }
        public string Desc { get; set; }
        public IEnumerable<Speaker> Speakers { get; set; }
    }
}
