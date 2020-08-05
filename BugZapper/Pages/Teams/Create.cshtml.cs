using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BugZapper.Data;
using BugZapper.Models;

namespace BugZapper.Pages.Teams
{
    public class CreateModel : PageModel
    {
        private readonly BugZapper.Data.BugZapperContext _context;

        public CreateModel(BugZapper.Data.BugZapperContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public TeamModel TeamModel { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.TeamModel.Add(TeamModel);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
