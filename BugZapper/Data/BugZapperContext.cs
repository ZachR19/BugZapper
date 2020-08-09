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

            //builder.ApplyConfiguration(new ProjectConfiguration());
        }

        public DbSet<ProjectModel> ProjectModel { get; set; }

        public DbSet<TeamModel> TeamModel { get; set; }
    }
}
