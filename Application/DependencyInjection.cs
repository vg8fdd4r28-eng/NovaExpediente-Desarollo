using System.Reflection;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NovaExpediente.Application.Common.Behaviours;
using NovaExpediente.Common;

namespace NovaExpediente.Application
{
    /// <summary>
    /// Configuración de inyección de dependencias para la capa de Application.
    /// Registra servicios de MediatR, AutoMapper y behaviors del pipeline.
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Registra todos los servicios de la capa de aplicación.
        /// </summary>
        /// <param name="services">Colección de servicios de ASP.NET Core</param>
        /// <returns>La colección de servicios para encadenamiento</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            // Registra AutoMapper para mapeo automático entre DTOs y entidades
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            
            // Registra MediatR para implementar patrón CQRS (Commands y Queries)
            services.AddMediatR(Assembly.GetExecutingAssembly());
            
            // Registra behavior para medir performance de requests
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            
            // Registra behavior para validación automática usando FluentValidation
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            
            return services;
        }
    }
}
