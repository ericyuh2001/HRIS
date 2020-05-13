using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
// using NLog.Web;

namespace HRIS_WAMS_WebCoreAPI
{
    public class Program
    {
        
         
        public static void Main(string[] args)
        {

                var host = CreateHostBuilder(args).Build();

                //ILogger logger = host.Services.GetRequiredService<ILogger<Program>>();
                //logger.LogInformation("HRIS_WAMS_WebCoreAPI system start ");


                host.Run();
            
        }



       


        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)                
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    // webBuilder.UseStartup<Startup>().ConfigureLogging(logging =>
                    // {
                    //     logging.ClearProviders();
                    //     logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    // })
                    // .UseNLog();
                });
    }
}
