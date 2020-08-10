using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BugZapper
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);

            //builder.ConfigureAppConfiguration(builder =>
            //{
            //    var root = builder.Build();
            //    var vaultName = root["KeyVault:Vault"];

            //    builder.AddAzureKeyVault($"https://{vaultName}.vault.azure.net/",
            //        root["KeyVault:ClientID"],
            //        root["KeyVault:Secret"]);
            //});

            builder.ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

            return builder;
        }
    }
}
