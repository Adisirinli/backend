using System.ComponentModel.DataAnnotations;

namespace BackEndProject_Edu.ViewModels
{
    public class ResetPasswordVM
    {
        [Required, MaxLength(100), DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, MaxLength(100), DataType(DataType.Password), Compare(nameof(Password))]
        public string RePassword { get; set; }
    }
}
