using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WebUI
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args).ConfigureLogging(logBuilder =>
            {
                logBuilder.ClearProviders(); // removes all providers from LoggerFactory
                logBuilder.AddConsole();
                logBuilder.AddTraceSource("Information, ActivityTracing"); // Add Trace listener provider
            })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }


        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<ApplicationDbContext>();

                    if (context.Database.IsSqlServer()) context.Database.Migrate();

                    await ApplicationDbContextSeed.SeedSampleDataAsync(context);
                }
                catch (Exception ex)
                {
                    ILogger logger = host.Services.GetService<ILogger<Program>>();

                    logger.LogError(ex, "An error occurred while migrating or seeding the database.");

                    throw;
                }
            }

            await host.RunAsync();
        }
    }
}
