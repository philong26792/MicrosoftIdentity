using System.Collections.Generic;
using System.Threading.Tasks;
using MicrosoftIdentity.API.Dtos;
using MicrosoftIdentity.API.Helpers;
using MicrosoftIdentity.API.Models.Identity;

namespace MicrosoftIdentity.API._Services.Interfaces
{
    public interface IAccountService
    {
        Task<PagedList<ApplicationUser>> GetData(PaginationParams param);
        Task<string> Register(RegisterViewModel model);
        Task<bool> Login(LoginViewModel user);
        Task<bool> DeleteUser(string userName);
        Task<bool> ChangePassword(string userName ,string passwordOld, string passwordNew);
        Task<bool> UpdateInformationUser(UserUpdateInformation data, string userName);
        Task<object> GetInformationUser(string userName);
    }
} 