using Microsoft.AspNetCore.Identity;

namespace Sporthall.Core.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
    }
}
