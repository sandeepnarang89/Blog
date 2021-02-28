using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Blogpost.API.Application.Impl;
using Blogpost.Core.Application;
using Blogpost.Infrasturcture.ORM;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

        }

        public static IWebHost BuildWebHost(string[] args)
        {
            var currentEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings." + currentEnv + ".json");

            var configuration = builder.Build();
            Settings.Default = new Settings(configuration);

            var hostBuilder = WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

            hostBuilder.ConfigureServices((WebHostBuilderContext context, IServiceCollection x) =>
            {
                x.AddSingleton<ISettings>(Settings.Default);
            });

            try
            {
                var hb = hostBuilder.Build();
                using (var scope = hb.Services.CreateScope())
                {
                    scope.ServiceProvider.GetService<ApplicationDbContext>();
                }
                return hb;
            }
            catch (Exception ex)
            {
                File.WriteAllText("ProgramError.txt", currentEnv + "-" + ex.ToString());
                throw new InvalidDataException("Error=" + ex);
            }
        }
    }
}
