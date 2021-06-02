using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MicrosoftIdentity.API._Services.Interfaces;
using MicrosoftIdentity.API.Dtos;
using MicrosoftIdentity.API.Models.Identity;

namespace MicrosoftIdentity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthController(IAccountService accountService,
                                    IConfiguration config,
                                    SignInManager<ApplicationUser> signInManager,
                                    UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _accountService = accountService;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel user)
        {
            var data = await _accountService.Login(user);
            if (data)
            {
                var userFind = await _userManager.FindByNameAsync(user.UserName);
                var userRole = await _userManager.GetRolesAsync(userFind);
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userFind.Id.ToString()),
                    new Claim(ClaimTypes.Name, userFind.UserName)
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8
                    .GetBytes(_config.GetSection("AppSettings:Token").Value));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return Ok(new
                {
                    token = tokenHandler.WriteToken(token),
                    user = userFind,
                    roles = userRole
                });
            }
            return Unauthorized();
        }
       
    }
}