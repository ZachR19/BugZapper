using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(BugZapper.Areas.Identity.IdentityHostingStartup))]
namespace BugZapper.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}