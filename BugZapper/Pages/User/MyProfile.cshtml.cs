using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using BugZapper.Data;
using Microsoft.EntityFrameworkCore;

namespace BugZapper.Pages.User
{
    public class MyProfileModel : PageModel
    {
        private UserManager<AppUser> _userMan { get; }
        private BugZapperContext _context { get; }

        public AppUser LoggedInUser { get; set; }

        public MyProfileModel(UserManager<AppUser> userman, BugZapperContext context)
        {
            _userMan = userman;
            _context = context;
        }

        public async Task OnGet()
        {
            if (HttpContext.User != null)
            {
                LoggedInUser = await _userMan.GetUserAsync(HttpContext.User);
            }
        }

        public async Task OnPost()
        {
            try
            {
                //Get logged in user
                LoggedInUser =  await _userMan.GetUserAsync(HttpContext.User);

                var user = await _context.Users.Where(u => u.Id == LoggedInUser.Id).FirstOrDefaultAsync();

                if (user != null)
                {
                    user.FirstName = Request.Form["FirstName"];
                    user.LastName = Request.Form["LastName"];
                    user.Bio = Request.Form["Bio"];
                    user.Company = Request.Form["Company"];
                    user.Role = Request.Form["Role"];
                    user.Location = Request.Form["Location"];
                    user.Website = Request.Form["Website"];

                    await _context.SaveChangesAsync();

                    //show success
                }
                else
                {
                    //show error message
                }
            }
            catch (Exception e)
            {
                //show error message
                Console.WriteLine(e);
                throw;
            }

        }
    }
}