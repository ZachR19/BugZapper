using BugZapper.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BugZapper.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider) 
        {
            using (var context = new BugZapperContext(
                serviceProvider.GetRequiredService<DbContextOptions<BugZapperContext>>()))
            {
                // Look for any movies.
                if (context.ProjectModel.Any())
                {
                    return;   // DB has been seeded
                }

                context.ProjectModel.AddRange(
                     new ProjectModel
                     {
                         ProjName = "Test1",
                         Description = "Some Desc",
                         CreatedBy = "Zach",
                         CreatedDate = DateTime.Today,
                         LastEditDate = DateTime.Today
                     },

                     new ProjectModel
                     {
                         ProjName = "Test2",
                         Description = "Some Desc",
                         CreatedBy = "Zach",
                         CreatedDate = DateTime.Today,
                         LastEditDate = DateTime.Today
                     },

                     new ProjectModel
                     {
                         ProjName = "Test3",
                         Description = "Some Desc",
                         CreatedBy = "Zach",
                         CreatedDate = DateTime.Today,
                         LastEditDate = DateTime.Today
                     },

                     new ProjectModel
                     {
                         ProjName = "Test4",
                         Description = "Some Desc",
                         CreatedBy = "Zach",
                         CreatedDate = DateTime.Today,
                         LastEditDate = DateTime.Today
                     }
                 );
                context.SaveChanges();
            }
        }

    }
}
