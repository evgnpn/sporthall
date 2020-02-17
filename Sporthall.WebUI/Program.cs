using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sporthall.Core.Identity.Managers;
using Sporthall.Core.Services;
using Sporthall.Infrastructure;
using System.Threading.Tasks;

namespace Sporthall.WebUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (IServiceScope servicesScope = host.Services.CreateScope())
            {
                var serviceProvider = servicesScope.ServiceProvider;

                var dbContext = serviceProvider.GetRequiredService<EfDbContext>();
                var roleManager = serviceProvider.GetRequiredService<AppRoleManager>();
                var servicesCollection = serviceProvider.GetRequiredService<AppServices>();

                //var initializer = new EfDbInitializer(dbContext, roleManager, servicesCollection);

                //await initializer.CreateDbIfNotExistsAsync();
                //await initializer.SeedTestDataAsync();
            }

            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
        }
    }
}
