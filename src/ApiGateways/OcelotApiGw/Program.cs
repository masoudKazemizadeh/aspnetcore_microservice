using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OcelotApiGw
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingBuilder, config) =>
                {
                    config.AddJsonFile($"ocelot.{hostingBuilder.HostingEnvironment.EnvironmentName}.json",true,true);
                    
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                }).ConfigureLogging((hostbuilder,loggingbuuiler)=> {
                    loggingbuuiler.AddConfiguration(hostbuilder.Configuration.GetSection("Logging"));
                    loggingbuuiler.AddDebug();
                    loggingbuuiler.AddConsole();
                });
    }
}
