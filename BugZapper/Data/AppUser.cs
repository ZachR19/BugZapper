using Microsoft.AspNetCore.Identity;

namespace BugZapper
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Bio { get; set; }
        public string Company { get; set; }
        public string Role { get; set; }
        public string Location { get; set; }
        public string Website { get; set; }
        public string IconName { get; set; }
    }
}
