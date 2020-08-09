using System.Linq;
using BugZapper.Data;
using BugZapper.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BugZapper.Pages.Teams
{
    public class CreateModel : PageModel
    {
        private readonly BugZapperContext _context;
        private readonly UserManager<AppUser> _userMan;

        public CreateModel(BugZapperContext context, UserManager<AppUser> userman)
        {
            _context = context;
            _userMan = userman;
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
                return Page();

            //Get logged in user
            var user = await _userMan.GetUserAsync(HttpContext.User);

            if (user == null) 
                return RedirectToPage("./Index");

            TeamModel.Owner_ID = user.Id;
            
            await  _context.TeamModel.AddAsync(TeamModel);

            //Save the team
            await _context.SaveChangesAsync();

            var perm = new TeamPermission()
            {
                TeamID = TeamModel.ID,
                UserID = user.Id,
                PermKey = "Admin",
                PermDescription = "This is the owner of this team"
            };

            user.TeamPerms.Add(perm);

            //Save the permission
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
