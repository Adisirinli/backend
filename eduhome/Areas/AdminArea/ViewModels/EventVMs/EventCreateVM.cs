using System.ComponentModel.DataAnnotations;

namespace BackEndProject_Edu.Areas.AdminArea.ViewModels.EventVMs
{
    public class EventCreateVM
    {

        [Required]
        public string Name { get; set; }
        [Required]

        public IFormFile ImgUrl { get; set; }
        [Required]

        public string Venue { get; set; }
        [Required]

        public string Desc { get; set; }
    }
}
