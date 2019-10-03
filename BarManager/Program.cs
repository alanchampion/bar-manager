using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BarManager.Models;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace BarManager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<BarManagerContext>();
                    // Using default recipe of old fashioned
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred creating the DB.");
                }
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((context, config) =>
                {
                    var tempConfig = new ConfigurationBuilder()
                        .AddJsonFile("C:\\Program Files\\Amazon\\ElasticBeanstalk\\config\\containerconfiguration", optional: true, reloadOnChange: true).Build();
                    var env = tempConfig.GetSection("iis:env").GetChildren();
                    List<string> variables = new List<string>();

                    foreach (var envKeyValue in env)
                    {
                        variables.Add(envKeyValue.Value);
                    }
                    config.AddCommandLine(variables.ToArray());
                    // config.AddJsonFile(@"C:\Program Files\Amazon\ElasticBeanstalk\config\containerconfiguration", optional: true, reloadOnChange: true);
                })
                .UseStartup<Startup>();
    }
}
