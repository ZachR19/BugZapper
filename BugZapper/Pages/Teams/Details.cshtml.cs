using BugZapper.Data;
using BugZapper.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BugZapper.Pages.Teams
{
    public class DetailsModel : PageModel
    {
        private readonly BugZapperContext _context;

        public DetailsModel(BugZapperContext context)
        {
            _context = context;
        }

        public TeamModel Team { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            Team = await _context.TeamModel
                            .Include(t => t.Projects)
                            .FirstOrDefaultAsync(m => m.ID == id);

            if (Team == null)
                return NotFound();

            return Page();
        }
    }
}
