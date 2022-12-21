using Contracts;
using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using static Contracts.Class1;
using static LoggerService.Class1;

namespace Main.Extensions {
    public static class ServiceExtensions {
        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddScoped<ILoggerManager, LoggerManager>();
        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(opts =>
        opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b =>
        b.MigrationsAssembly("Main")));
        public static void ConfigureRepositoryManager(this IServiceCollection services)
        =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();
        public static IMvcBuilder AddCustomCSVFormatter(this IMvcBuilder builder) =>
builder.AddMvcOptions(config => config.OutputFormatters.Add(new
CsvOutputFormatter()));
        public static IMvcBuilder AddCustomCSVFormatter2(this IMvcBuilder builder) =>
builder.AddMvcOptions(config => config.OutputFormatters.Add(new
CsvOutputFormatter2()));
        public static void ConfigureVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });
        }
    }

}