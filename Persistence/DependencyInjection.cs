using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NovaExpediente.Application.Common.Interfaces;


namespace NovaExpediente.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<NovaExpedienteDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("NovaExpedienteDatabase")));

            services.AddScoped<INovaExpedienteDbContext>(provider => provider.GetService<NovaExpedienteDbContext>());
            services.AddTransient<IFireForgetCommandHandler, FireForgetCommandHandler>();
            services.AddScoped<ILoggingService, LoggingService>();
            return services;
        }
    }
}
