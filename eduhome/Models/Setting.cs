using System.ComponentModel.DataAnnotations;

namespace BackEndProject_Edu.Models
{
    public class Setting : BaseEntity
    {
        [Required]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
