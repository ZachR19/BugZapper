using System.Collections.Generic;

namespace BugZapper.Models
{
    /// <summary>
    /// Represents individual users
    /// </summary>
    public class UserModel
    {
        public string Username { get; set; }
        public string UserID { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// All Teams this user is associated with
        /// </summary>
        public List<TeamModel> Teams { get; set; }

        /// <summary>
        /// The permissions this user has for interacting with their teams and projects
        /// </summary>
        //public List<Permission> Permissions { get; set; }
    }
}
