using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace BugZapper.Pages.Shared.Components.Navbar
{
    public class Navbar : ViewComponent
    {
        public Navbar()
        {

        }

        public IViewComponentResult Invoke(string URL)
        {
            var url = Request.GetDisplayUrl().ToLower();
            
            if (url.Contains("project"))
                return View("Navbar", "projects");
            else if (url.Contains("team"))
                return View("Navbar", "teams");

            return View("Navbar", "home");
        }

    }
}
