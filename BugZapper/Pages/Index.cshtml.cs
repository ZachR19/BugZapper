using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace BugZapper.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly UserManager<AppUser> _userMan;

        private readonly SignInManager<AppUser> _signinMan;

        public IndexModel(ILogger<IndexModel> logger, 
                          UserManager<AppUser> userMan, 
                          SignInManager<AppUser> signinMan)
        {
            _logger = logger;
            _userMan = userMan;
            _signinMan = signinMan;
        }

        [BindProperty(SupportsGet = true)]
        public string Username { get; set; }

        public void OnGet()
        { 
            //Display username in index page title
            var claim = HttpContext.User;

            Username = claim != null ? claim.Identity.Name : string.Empty;
        }

    }
}
