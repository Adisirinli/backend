
using System.ComponentModel.DataAnnotations;

namespace BackEndProject_Edu.Areas.AdminArea.ViewModels.SliderVMs
{
    public class SliderCreateVM
    {
        [Required]


        public IFormFile Photo { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Desc { get; set; }
    }
}
