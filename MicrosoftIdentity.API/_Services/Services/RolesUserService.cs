using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MicrosoftIdentity.API._Services.Interfaces;
using MicrosoftIdentity.API.Models.Identity;

namespace MicrosoftIdentity.API._Services.Services
{
    public class RolesUserService : IRolesUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<AppRoles> _roleManager;
        public RolesUserService(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                RoleManager<AppRoles> roleManager) {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<bool> CreateUserRole(string role, string user)
        {
            var checkUser = await _userManager.FindByNameAsync(user);
            var result = await _userManager.AddToRoleAsync(checkUser, role);
            return result.Succeeded;
        }

        public async Task<bool> UpdateUserRole(string role, string userName)
        {
            var userFind = await _userManager.FindByNameAsync(userName);
            var roles = await _userManager.GetRolesAsync(userFind);
            var deleteRoleOfUser = await _userManager.RemoveFromRoleAsync(userFind, roles[0]);
            var addRoleOfUser = await _userManager.AddToRoleAsync(userFind, role);
            if(deleteRoleOfUser.Succeeded && addRoleOfUser.Succeeded) {
                return true;
            }
            return false;
        }
    }
}