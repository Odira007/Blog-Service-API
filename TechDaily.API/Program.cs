using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using System;
using System.IO;

namespace TechDaily.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    Log.Logger = new LoggerConfiguration()
                        .WriteTo.File(new JsonFormatter(), Path.Combine(Environment.CurrentDirectory, "logs", 
                            "techdaily-important.json"), restrictedToMinimumLevel: LogEventLevel.Warning)
                        .WriteTo.File(Path.Combine(Environment.CurrentDirectory, "logs", "techdaily-.logs"), 
                            rollingInterval: RollingInterval.Day)
                        .MinimumLevel.Debug()
                        .CreateLogger();

                    webBuilder.UseStartup<Startup>();
                });
    }
}
