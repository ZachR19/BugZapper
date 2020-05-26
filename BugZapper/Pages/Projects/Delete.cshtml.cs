using BugZapper.Data;
using BugZapper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BugZapper.Pages.Projects
{
    public class DeleteModel : PageModel
    {
        private readonly BugZapperContext _context;

        public DeleteModel(BugZapperContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            ProjectModel = await _context.ProjectModel.FindAsync(id);

            if (ProjectModel != null)
            {
                _context.ProjectModel.Remove(ProjectModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
