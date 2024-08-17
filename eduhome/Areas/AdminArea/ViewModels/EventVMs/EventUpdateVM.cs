namespace BackEndProject_Edu.Areas.AdminArea.ViewModels.EventVMs
{
    public class EventUpdateVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImgUrl { get; set; }
        public IFormFile Photo { get; set; }
        public DateTime Time { get; set; }
        public string Venue { get; set; }
        public string Desc { get; set; }
    }
}
