using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BugZapper.Models;

namespace BugZapper.Data
{
    public class BugZapperContext : DbContext
    {
        public BugZapperContext (DbContextOptions<BugZapperContext> options)
            : base(options)
        {
        }

        public DbSet<BugZapper.Models.ProjectModel> ProjectModel { get; set; }
    }
}
