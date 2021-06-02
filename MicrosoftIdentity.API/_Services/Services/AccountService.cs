
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MicrosoftIdentity.API._Services.Interfaces;
using MicrosoftIdentity.API.Dtos;
using MicrosoftIdentity.API.Helpers;
using MicrosoftIdentity.API.Models.Identity;

namespace MicrosoftIdentity.API._Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<AppRoles> _roleManager;
        
        public AccountService(  UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                RoleManager<AppRoles> roleManager) {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public async Task<bool> ChangePassword(string userName, string passwordOld, string passwordNew)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.ChangePasswordAsync(user, passwordOld, passwordNew);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUser(string userName)
        {
            var userFind = await _userManager.FindByNameAsync(userName);
            var result = await _userManager.DeleteAsync(userFind);
            return result.Succeeded;
        }

        public async Task<PagedList<ApplicationUser>> GetData(PaginationParams param)
        {
            var data = _userManager.Users;
            return await PagedList<ApplicationUser>.CreateAsync(data, param.PageNumber, param.PageSize);
        }

        public async Task<object> GetInformationUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            var role = await _userManager.GetRolesAsync(user);
            return new {
                user = user,
                role = role
            };
        }

        public async Task<bool> Login(LoginViewModel user)
        {
            var result = await _signInManager.PasswordSignInAsync(user.UserName, user.Password, false, false);
            return result.Succeeded;
        }


        public async Task<string> Register(RegisterViewModel model)
        {
            var data = "";
            // Check username trong database
            ApplicationUser userFind = await _userManager.FindByNameAsync(model.UserName);
            if (userFind != null) {
                data = "exist";
            }
            else {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded) {
                    // await _signInManager.SignInAsync(user, isPersistent: false);
                    await _userManager.AddToRoleAsync(user, "user");
                    data = "ok";
                } else {
                    foreach (var error in result.Errors) {
                        data = data + error.ToString() + " - ";
                    }
                }
                
            }
            return data;
        }

        public async Task<bool> UpdateInformationUser(UserUpdateInformation data, string userName)
        {
            var user = await _userManager.FindByNameAsync(userName.Trim());
            if(user != null) {
                user.PhoneNumber = data.PhoneNumber;
                user.FirstName = data.FirstName;
                user.LastName = data.LastName;
                user.Age = data.Age;
                var resultUpdate = await _userManager.UpdateAsync(user);
                return resultUpdate.Succeeded;
            }
            return false;
        }
    }
}