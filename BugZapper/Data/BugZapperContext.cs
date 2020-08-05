using Microsoft.EntityFrameworkCore;
using BugZapper.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BugZapper.Data
{
    public class BugZapperContext : IdentityDbContext<AppUser>
    {
        public BugZapperContext (DbContextOptions<BugZapperContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectModel> ProjectModel { get; set; }

        public DbSet<BugZapper.Models.TeamModel> TeamModel { get; set; }
    }
}
