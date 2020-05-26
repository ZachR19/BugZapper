using BugZapper.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BugZapper.Data;

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

        public async Task OnGetAsync()
        {
            ProjectModel = await _context.ProjectModel.ToListAsync();
        }
    }
}
