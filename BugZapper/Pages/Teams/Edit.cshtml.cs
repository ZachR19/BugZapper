using BugZapper.Data;
using BugZapper.Models;
using BugZapper.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BugZapper.Pages.Teams
{
    public class EditModel : PageModel
    {
        private readonly BugZapperContext _context;
        private readonly IUserService _userService;

        public EditModel(BugZapperContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }

        [BindProperty]
        public TeamModel Team { get; set; }

        public AppUser User { get; set; }

        public bool AllowEdit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            User = await _userService.GetUserByClaims(HttpContext.User);
            Team = await _context.TeamModel.FirstOrDefaultAsync(m => m.ID == id);

            if (User == null || Team == null)
                return NotFound();

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Attach(Team).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamModelExists(Team.ID))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToPage("./Index");
        }

        private bool TeamModelExists(int id)
        {
            return _context.TeamModel.Any(e => e.ID == id);
        }
    }
}
