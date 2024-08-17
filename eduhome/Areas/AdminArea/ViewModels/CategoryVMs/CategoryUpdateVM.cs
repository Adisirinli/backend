using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackEndProject_Edu.Areas.AdminArea.ViewModels.CategoryVMs
{
    public class CategoryUpdateVM
    {
        [Required, MaxLength(10)]
        [DisplayName("CategoryName")]
        public string Name { get; set; }

    }
}
