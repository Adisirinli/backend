using System.ComponentModel.DataAnnotations;

namespace BackEndProject_Edu.Areas.AdminArea.ViewModels.UserVMs
{
    public class UserPasswordVM
    {
        [Required, MaxLength(100), DataType(DataType.Password)]

        public string CurrentPassword { get; set; }
        [Required, MaxLength(100), DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [Required, MaxLength(100), DataType(DataType.Password), Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
    }
}
