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

namespace BugZapper.Pages.Teams
{
    public class EditModel : PageModel
    {
        private readonly BugZapper.Data.BugZapperContext _context;

        public EditModel(BugZapper.Data.BugZapperContext context)
        {
            _context = context;
        }

        [BindProperty]
        public TeamModel TeamModel { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TeamModel = await _context.TeamModel.FirstOrDefaultAsync(m => m.ID == id);

            if (TeamModel == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(TeamModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamModelExists(TeamModel.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool TeamModelExists(int id)
        {
            return _context.TeamModel.Any(e => e.ID == id);
        }
    }
}
