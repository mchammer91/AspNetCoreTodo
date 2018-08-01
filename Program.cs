using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using AspNetCoreTodo.Data;
using Microsoft.Extensions.DependencyInjection;

namespace AspNetCoreTodo
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = BuildWebHost(args);
            await InitializeDatabase(host);
            host.Run();
        }

        private static async Task InitializeDatabase(IWebHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    await SeedData.InitializeAsync(services);
                }
                catch (Exception e)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(e, "Error occurred seeding the DB");
                }
            }
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .Build();
    }
}
