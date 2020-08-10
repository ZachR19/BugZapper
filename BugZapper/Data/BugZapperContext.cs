using BugZapper.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BugZapper.Data
{
    public class BugZapperContext : IdentityDbContext<AppUser>
    {
        public BugZapperContext (DbContextOptions<BugZapperContext> options)
            : base(options)
        {
        }

        public DbSet<ProjectModel> ProjectModel { get; set; }

        public DbSet<TeamModel> TeamModel { get; set; }

        public DbSet<TeamPermission> TeamPermission { get; set; }

        public DbSet<ProjectPermission> ProjectPermission { get; set; }
    }
}
