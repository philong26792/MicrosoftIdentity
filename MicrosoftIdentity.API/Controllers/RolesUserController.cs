using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MicrosoftIdentity.API._Services.Interfaces;
using MicrosoftIdentity.API.Models.Identity;

namespace MicrosoftIdentity.API.Controllers
{
    [Route("api/[controller]")]
    // [Authorize]
    [ApiController]
    public class RolesUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<AppRoles> _roleManager;
        private readonly IRolesUserService _rolesUserService;
        public RolesUserController(  UserManager<ApplicationUser> userManager,
                                    SignInManager<ApplicationUser> signInManager,
                                    RoleManager<AppRoles> roleManager,
                                    IRolesUserService rolesUserService) {
                _userManager = userManager;
                _signInManager = signInManager;
                _roleManager = roleManager;
                _rolesUserService = rolesUserService;
        }

        [HttpPost("createUserRole")]
        public async Task<IActionResult> CreateUserRole(string role, string user) {
            var result = await _rolesUserService.CreateUserRole(role,user);
            return Ok(result);
        }

        [HttpGet("updateUserRole")]
        public async Task<IActionResult> UpdateUserRole(string role, string userName) {
            var result = await _rolesUserService.UpdateUserRole(role, userName);
            return Ok(result);
        }
    }
}