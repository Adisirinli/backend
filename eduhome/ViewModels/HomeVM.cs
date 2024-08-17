using BackEndProject_Edu.Models;

namespace BackEndProject_Edu.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Blog> Blogs { get; set; }
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }

    }
}
