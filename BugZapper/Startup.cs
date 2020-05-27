using BugZapper.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Options;

namespace BugZapper
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
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

            CreateDBConnection(services);

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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Setup Identity
            app.UseAuthentication();

            if (env.IsDevelopment())
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

        /// <summary>
        /// Builds the connection string and initiates connection to the database
        /// </summary>
        /// <param name="services"></param>
        private void CreateDBConnection(IServiceCollection services)
        {
            var ConnString = Configuration.GetConnectionString("BugZapperContext");

            ConnString = ConnString.Replace("Server=", $"Server={Configuration["Value1"]}");
            ConnString = ConnString.Replace("Initial Catalog=", $"Initial Catalog={Configuration["Value2"]}");
            ConnString = ConnString.Replace("User ID=", $"User ID={Configuration["Value3"]}");
            ConnString = ConnString.Replace("Password=", $"Password={Configuration["Value4"]}");

            services.AddDbContext<BugZapperContext>(options =>
                options.UseSqlServer(ConnString));

        }

        private void ConfigureExternalLoginServices(IServiceCollection services)
        {
            //Allow Facebook logins
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = Configuration["Authentication:Facebook:AppId"];
                options.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            //Allow Google logins
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = Configuration["Authentication:Google:ClientId"];
                options.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });
        }
    }
}
