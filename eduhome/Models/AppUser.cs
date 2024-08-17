using Microsoft.AspNetCore.Identity;

namespace BackEndProject_Edu.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
        public bool IsBlocked { get; set; }
        public string ConnectionId { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Basket Basket { get; set; }

    }
}
