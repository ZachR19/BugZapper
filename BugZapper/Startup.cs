using BugZapper.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Azure.Core;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

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
                options.UseSqlServer(GetConfigValue("DB")));
            
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

        private string GetConfigValue(string Key)
        {
            //Get config data from Azure
            SecretClientOptions options = new SecretClientOptions()
            {
                Retry =
                {
                    Delay= TimeSpan.FromSeconds(1),
                    MaxDelay = TimeSpan.FromSeconds(15),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                }
            };

            var client = new SecretClient(new Uri("https://bugzapperkeyvault.vault.azure.net/"), new DefaultAzureCredential(), options);

            KeyVaultSecret secret = client.GetSecret(Key);

            return secret.Value;
        }

        private void ConfigureExternalLoginServices(IServiceCollection services)
        {
            //Allow Facebook logins
            services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = GetConfigValue("FacebookAppId");
                options.AppSecret = GetConfigValue("FacebookAppSecret");
            });

            //Allow Google logins
            services.AddAuthentication().AddGoogle(options =>
            {
                options.ClientId = GetConfigValue("GoogleClientId");
                options.ClientSecret = GetConfigValue("GoogleClientSecret");
            });
        }
    }
}
