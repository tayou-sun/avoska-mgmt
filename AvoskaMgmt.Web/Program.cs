using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AvoskaMgmt.Web
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
                     webBuilder.UseUrls("https://*:51810", "http://*:51811");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
