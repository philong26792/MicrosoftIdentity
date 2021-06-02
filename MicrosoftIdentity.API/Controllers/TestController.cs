using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MicrosoftIdentity.API.Models.Identity;

namespace MicrosoftIdentity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<AppRoles> _roleManager;
        public TestController(  UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                RoleManager<AppRoles> roleManager) {
                _userManager = userManager;
                _signInManager = signInManager;
                _roleManager = roleManager;
        }

        [HttpGet("test")]
        public async Task<IActionResult> GetData() {
            var data1 = await _userManager.Users.ToListAsync();
            // var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();
            // var userFind = await _userManager.FindByNameAsync("longvjp1992");
            // var test1 = await _signInManager.CreateUserPrincipalAsync(userFind);
            // var a = _signInManager.IsSignedIn(test1);
            return Ok(data1);
        }
    }
}