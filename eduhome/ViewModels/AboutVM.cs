using BackEndProject_Edu.Models;

namespace BackEndProject_Edu.ViewModels
{
    public class AboutVM
    {
        public Dictionary<string, string> KeyValues { get; set; }
        public IEnumerable<Teacher> Teachers { get; set; }
        public IEnumerable<Event> Events { get; set; }
    }
}
