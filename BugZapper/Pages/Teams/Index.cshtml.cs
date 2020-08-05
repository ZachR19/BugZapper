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

        public IList<TeamModel> TeamModel { get;set; }

        public async Task OnGetAsync()
        {
            TeamModel = await _context.TeamModel.ToListAsync();
        }
    }
}
