using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MicrosoftIdentity.API._Services.Interfaces;
using MicrosoftIdentity.API.Models.Identity;

namespace MicrosoftIdentity.API._Services.Services
{
    public class SettingRoleService : ISettingRoleService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<AppRoles> _roleManager;
        public SettingRoleService(  UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                RoleManager<AppRoles> roleManager) {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        public async Task<string> CreateRole(AppRoles model)
        {
            var roleCheck = await _roleManager.RoleExistsAsync(model.Name.Trim());
            if(roleCheck) {
                return "exist";
            } else {
                var result = await _roleManager.CreateAsync(model);
                if(result.Succeeded) {
                    return "ok";
                } else {
                    return "error";
                }
            }
        }

        public async Task<bool> DeleteRole(string roleName)
        {
            var roleFind = await _roleManager.FindByNameAsync(roleName.Trim());
            var removed =  await _roleManager.DeleteAsync(roleFind);
            return removed.Succeeded;
        }

        public async Task<List<AppRoles>> GetAll()
        {
            var data =  await _roleManager.Roles.ToListAsync();
            return data;
        }

        public async Task<bool> UpdateRole(AppRoles model)
        {
            var roleFind = await _roleManager.FindByIdAsync(model.Id.Trim());
            roleFind.Name = model.Name.Trim();
            roleFind.Status = model.Status;
            var updated =  await _roleManager.UpdateAsync(roleFind);
            return updated.Succeeded;
        }
    }
}