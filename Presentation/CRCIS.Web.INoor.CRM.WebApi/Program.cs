using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CRCIS.Web.INoor.CRM.Infrastructure.LoggerProvider;
using CRCIS.Web.INoor.CRM.Infrastructure.Extensions;
using CRCIS.Web.INoor.CRM.Infrastructure.Settings;

namespace CRCIS.Web.INoor.CRM.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

            CreateHostBuilder(args, config).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args, IConfigurationRoot config) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    //logging.ClearProviders();
                    logging.AddConsole();
                    logging.AddInDbLogger();
                    logging.AddSentry(dsn: config.GetSection("Sentry").Get<SentrySettings>().Dsn);
                })
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseSentry(options=> options.Environment = "production");
                webBuilder.UseStartup<Startup>();
            });
    }
}
