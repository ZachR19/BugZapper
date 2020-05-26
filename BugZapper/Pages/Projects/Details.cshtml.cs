using BugZapper.Data;
using BugZapper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BugZapper.Pages.Projects
{
    public class DetailsModel : PageModel
    {
        private readonly BugZapperContext _context;

        public DetailsModel(BugZapperContext context)
        {
            _context = context;
        }

        public ProjectModel ProjectModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            ProjectModel = await _context.ProjectModel.FirstOrDefaultAsync(m => m.ID == id);

            if (ProjectModel == null)
                return NotFound();

            return Page();
        }
    }
}
