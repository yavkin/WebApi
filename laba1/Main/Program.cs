using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main {
    public static class Program {
        public static void Main(string[] args) {
            CreateHostBuilder(args).Build().Run();
        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => {
                    webBuilder.UseStartup<Startup>();
                });
        public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options => {
            options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin() .AllowAnyMethod() .AllowAnyHeader());
        });
        public static void ConfigureIISIntegration(this IServiceCollection services) => services.Configure<IISOptions>(options => {
        });
    }
}