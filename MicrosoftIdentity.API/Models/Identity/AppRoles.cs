using Microsoft.AspNetCore.Identity;

namespace MicrosoftIdentity.API.Models.Identity
{
    public class AppRoles : IdentityRole
    {
        public bool? Status {get;set;}
    }
}