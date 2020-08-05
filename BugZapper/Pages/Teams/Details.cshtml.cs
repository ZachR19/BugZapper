using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BugZapper.Data;
using BugZapper.Models;

namespace BugZapper.Pages.Teams
{
    public class DetailsModel : PageModel
    {
        private readonly BugZapper.Data.BugZapperContext _context;

        public DetailsModel(BugZapper.Data.BugZapperContext context)
        {
            _context = context;
        }

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
    }
}
