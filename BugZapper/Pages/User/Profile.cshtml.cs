using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BugZapper.Data;
using BugZapper.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BugZapper.Pages.User
{
    [BindProperties(SupportsGet = true)]
    public class ProfileModel : PageModel
    {
        private readonly BugZapperContext _context;

        public ProfileModel(BugZapperContext context)
        {
            _context = context;
        }

        [Parameter]
        public string ID { get; set; }

        public AppUser FoundUser { get; set; }

        public async Task OnGet()
        {
            if (ID != null)
            {
                try
                {
                    FoundUser = await _context.Users.Where(user => user.Id == this.ID).FirstOrDefaultAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}