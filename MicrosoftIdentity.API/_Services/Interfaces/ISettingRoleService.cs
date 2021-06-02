using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using MicrosoftIdentity.API.Models.Identity;

namespace MicrosoftIdentity.API._Services.Interfaces
{
    public interface ISettingRoleService
    {
        Task<List<AppRoles>> GetAll();
        Task<string> CreateRole(AppRoles model);
        Task<bool> DeleteRole(string roleName);
        Task<bool> UpdateRole(AppRoles model);
    }
}