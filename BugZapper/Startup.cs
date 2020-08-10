using BugZapper.Data;
using BugZapper.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace BugZapper
{
    public class Startup
    {
        private IWebHostEnvironment _env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();

            //Uses cookies to authenticate user and adds user info to HttpContext.User.Identity
            //Also adds various Identity Services (PasswordHasher, UserManagers, etc.)
            services.AddIdentity<AppUser, IdentityRole>()
                //Add UserStore and RoleStore based on the given context
                .AddEntityFrameworkStores<BugZapperContext>()
                //Creates unique keys for authentication purposes
                .AddDefaultTokenProviders();

            services.AddDbContext<BugZapperContext>(options =>
                options.UseSqlServer(AzureKeyVaultService.GetVaultValue("DB")));
            
            //Alter password policy
            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireNonAlphanumeric = false;
            });

            //Alter cookie information
            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Forms/Login";

                //Change cookie expiration time
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            });

            ConfigureExternalLoginServices(services);

            services.AddScoped<IUserService, UserService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app)
        {
            //Setup Identity
            app.UseAuthentication();

            if (_env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }

        private void ConfigureExternalLoginServices(IServiceCollection services)
        {
            //Allow Facebook logins
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = AzureKeyVaultService.GetVaultValue("FacebookAppId");
                options.AppSecret = AzureKeyVaultService.GetVaultValue("FacebookAppSecret");
            });

            //Allow Google logins
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = AzureKeyVaultService.GetVaultValue("GoogleClientId");
                options.ClientSecret = AzureKeyVaultService.GetVaultValue("GoogleClientSecret");
            });
        }
    }
}
