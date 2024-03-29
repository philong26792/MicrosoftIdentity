using Microsoft.AspNetCore.Identity;

namespace MicrosoftIdentity.API.Models.Identity
{
    public class ApplicationUser : IdentityUser
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age {get;set;}
        public bool? status {get;set;}
    }
}