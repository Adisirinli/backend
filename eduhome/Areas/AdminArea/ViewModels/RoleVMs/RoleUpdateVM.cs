using Microsoft.AspNetCore.Identity;

namespace BackEndProject_Edu.Areas.AdminArea.ViewModels.RoleVMs
{
    public class RoleUpdateVM
    {
        public RoleUpdateVM(string userName, List<IdentityRole> roles, IList<string> userRoles)
        {
            UserName = userName;
            Roles = roles;
            UserRoles = userRoles;
        }
        public string UserName { get; set; }
        public List<IdentityRole> Roles { get; set; }
        public IList<string> UserRoles { get; set; }
    }
}
