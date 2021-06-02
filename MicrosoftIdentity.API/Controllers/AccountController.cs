
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MicrosoftIdentity.API._Services.Interfaces;
using MicrosoftIdentity.API.Dtos;
using MicrosoftIdentity.API.Helpers;
using MicrosoftIdentity.API.Models.Identity;

namespace MicrosoftIdentity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAccountService _accountService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(   IAccountService accountService,
                                    IConfiguration config,
                                    SignInManager<ApplicationUser> signInManager,
                                    UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _accountService = accountService;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetData([FromQuery]PaginationParams param) {
            var data = await _accountService.GetData(param);
            Response.AddPagination(data.CurrentPage, data.PageSize, data.TotalCount, data.TotalPages);
            return Ok(data);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var result = await _accountService.Register(model);
            return Ok(new {data = result});
        }

        [HttpGet("getInformationUser")]
        public async Task<IActionResult> GetInformationUser(string userName) {
            var param = userName == "currentUser" ? User.FindFirst(ClaimTypes.Name).Value : userName;
            var user = await _accountService.GetInformationUser(param);
            return Ok(user);
        }

        [HttpDelete("removeUser")]
        public async Task<IActionResult> RemoveUser(string userName) {
            var result = await _accountService.DeleteUser(userName);
            return Ok(result);
        }
        [HttpGet("logout")]
        public async void Logout()
        {
            await _signInManager.SignOutAsync();
        }

        [HttpPost("changePass")]
        public async Task<IActionResult> ChangePassword(UserChangePass model) {
            var userName = User.FindFirst(ClaimTypes.Name).Value;
            var result = await _accountService.ChangePassword(userName, model.passwordOld, model.passwordNew);
            return Ok(result);
        }


        [HttpPost("updateInformationUser")]
        public async Task<IActionResult> UpdateInformationUser(UserUpdateInformation data) {
            var userCurrent = User.FindFirst(ClaimTypes.Name).Value;
            var result = await _accountService.UpdateInformationUser(data, userCurrent);
            return Ok(result);
        }

    }
}