using System.Threading.Tasks;

namespace MicrosoftIdentity.API._Services.Interfaces
{
    public interface IRolesUserService
    {
        Task<bool> CreateUserRole (string role, string user);
        Task<bool> UpdateUserRole(string role, string userName);
    }
}