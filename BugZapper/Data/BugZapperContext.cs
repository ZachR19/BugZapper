using BugZapper.Migrations.Configurations;
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<ProjectModel> ProjectModel { get; set; }

        public DbSet<TeamModel> TeamModel { get; set; }

        public DbSet<TeamPermission> TeamPermission { get; set; }

        public DbSet<ProjectPermission> ProjectPermission { get; set; }
    }
}
