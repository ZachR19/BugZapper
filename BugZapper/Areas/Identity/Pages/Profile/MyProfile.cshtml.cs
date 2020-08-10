using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using BugZapper.Data;
using BugZapper.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

namespace BugZapper.Pages.User
{
    public class MyProfileModel : PageModel
    {
        private IUserService _userService { get; }
        private BugZapperContext _context { get; }

        public readonly IWebHostEnvironment _env;

        public AppUser LoggedInUser { get; set; }
        public bool ShowUpdateSuccess { get; set; }
        public bool ShowUpdateFailure { get; set; }

        public MyProfileModel(IWebHostEnvironment env, IUserService userService, BugZapperContext context)
        {
            _env = env;
            _userService = userService;
            _context = context; 
        }

        public async Task OnGet()
        {
            LoggedInUser = await _userService.GetUserByClaims(HttpContext.User);
        }

        public async Task OnPost()
        {
            try
            {
                //Get logged in user
                LoggedInUser =  await _userService.GetUserByClaims(HttpContext.User);

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
                    ShowUpdateSuccess = true;
                    ShowUpdateFailure = false;
                }
                else
                {
                    //show error message
                    ShowUpdateFailure = true;
                    ShowUpdateSuccess = false;
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