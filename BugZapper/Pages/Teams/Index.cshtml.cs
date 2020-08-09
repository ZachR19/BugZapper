using BugZapper.Data;
using BugZapper.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BugZapper.Pages.Teams
{
    public class IndexModel : PageModel
    {
        private readonly BugZapperContext _context;

        public IndexModel(BugZapperContext context)
        {
            _context = context;
        }

        public IList<TeamModel> Teams { get;set; }

        public async Task OnGetAsync()
        {
            Teams = await _context.TeamModel.ToListAsync();
        }
    }
}
