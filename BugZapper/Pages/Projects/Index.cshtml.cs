using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BugZapper.Data;
using BugZapper.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BugZapper.Pages.Projects
{
    public class IndexModel : PageModel
    {
        private readonly BugZapperContext _context;

        public IndexModel(BugZapperContext context)
        {
            _context = context;
        }

        public IList<ProjectModel> ProjectModel { get;set; }

        #region Searching
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        public SelectList Creators { get; set; }
        [BindProperty(SupportsGet = true)]

        public string CreatorFilter { get; set; }
        #endregion

        public async Task OnGetAsync()
        {
            //Get all projects
            var projects = from m in _context.ProjectModel
                           select m;

            //Get list of creators
            IQueryable<string> creatorsQuery = from m in _context.ProjectModel
                                            orderby m.CreatedBy
                                            select m.CreatedBy;

            if (!string.IsNullOrEmpty(SearchString))
                projects = projects.Where(s => s.ProjName.Contains(SearchString));

            if (!string.IsNullOrEmpty(CreatorFilter))
                projects = projects.Where(x => x.CreatedBy == CreatorFilter);

            Creators = new SelectList(await creatorsQuery.Distinct().ToListAsync());

            ProjectModel = await projects.ToListAsync();
        }
    }
}
