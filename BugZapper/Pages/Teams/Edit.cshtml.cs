using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BugZapper.Data;
using BugZapper.Models;
using Microsoft.AspNetCore.Identity;

namespace BugZapper.Pages.Teams
{
    public class EditModel : PageModel
    {
        private readonly BugZapperContext _context;
        private readonly UserManager<AppUser> _userMan;

        public EditModel(BugZapperContext context, UserManager<AppUser> userman)
        {
            _context = context;
            _userMan = userman;
        }

        [BindProperty]
        public TeamModel Team { get; set; }

        public AppUser User { get; set; }

        public bool AllowEdit { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
                return NotFound();

            User = await _userMan.GetUserAsync(HttpContext.User);

            if (User == null)
                return NotFound();

            Team = await _context.TeamModel.FirstOrDefaultAsync(m => m.ID == id);

            if (Team == null)
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
