using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NovaExpediente.Application.Common.Interfaces;


namespace NovaExpediente.Persistence
{
    /// <summary>
    /// Configuración de inyección de dependencias para la capa de Persistence.
    /// Registra el contexto de Entity Framework y servicios de acceso a datos.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registra todos los servicios de persistencia.
        /// </summary>
        /// <param name="services">Colección de servicios</param>
        /// <param name="configuration">Configuración de la aplicación</param>
        /// <returns>La colección de servicios para encadenamiento</returns>
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            // Registra el contexto de Entity Framework con SQL Server
            services.AddDbContext<NovaExpedienteDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("NovaExpedienteDatabase")));

            // Registra la interfaz del contexto para inyección de dependencias
            services.AddScoped<INovaExpedienteDbContext>(provider => provider.GetService<NovaExpedienteDbContext>());
            
            // Registra handler para comandos fire-and-forget (ejecución asíncrona sin esperar respuesta)
            services.AddTransient<IFireForgetCommandHandler, FireForgetCommandHandler>();
            
            // Registra servicio de logging para persistir logs en base de datos
            services.AddScoped<ILoggingService, LoggingService>();
            
            return services;
        }
    }
}
