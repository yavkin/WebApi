using Microsoft.Extensions.DependencyInjection;
using static Contracts.Class1;
using static LoggerService.Class1;

namespace Main.Extensions {
    public static class ServiceExtensions {
        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddScoped<ILoggerManager, LoggerManager>();
    }
}