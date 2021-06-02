using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MicrosoftIdentity.API._Services.Interfaces;
using MicrosoftIdentity.API.Models.Identity;

namespace MicrosoftIdentity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingRolesController : ControllerBase
    {
        private readonly ISettingRoleService _settingRoleService;
        public SettingRolesController( ISettingRoleService settingRoleService) {
            _settingRoleService = settingRoleService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll() {
            var data = await _settingRoleService.GetAll();
            return Ok(data);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(AppRoles model) {
            var result = await _settingRoleService.CreateRole(model);
            return Ok(new {data = result});
        }
        
        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(string roleName) {
            var result = await _settingRoleService.DeleteRole(roleName);
            return Ok(result);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(AppRoles model) {
            var result = await _settingRoleService.UpdateRole(model);
            return Ok(result);
        }
    }
}