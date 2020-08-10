using BugZapper.Data;
using BugZapper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BugZapper.Pages.Teams
{
    public class DeleteModel : PageModel
    {
        private readonly BugZapperContext _context;

        public DeleteModel(BugZapperContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TeamModel TeamModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            TeamModel = await _context.TeamModel.FirstOrDefaultAsync(m => m.ID == id);

            if (TeamModel == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
                return NotFound();

            TeamModel = await _context.TeamModel.FindAsync(id);

            if (TeamModel != null)
            {
                _context.TeamModel.Remove(TeamModel);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
