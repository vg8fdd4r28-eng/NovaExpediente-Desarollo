using Effort;
using MediatR;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Infrastructure.Identity;
using NovaExpediente.Persistence;
using NovaExpediente.WebUI;
using NUnit.Framework;
using Respawn;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Integration.Tests
{
    [SetUpFixture]
    public class Testing
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        //private static Checkpoint _checkpoint;
        private static int? _currentUserId;

        #region Configuración del ambiente de testing
        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            // parámetros de configuración
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            // colección de servicios que se agregan dinámicamente para 
            // generar el contexto de ejecución de la aplicación
            var services = new ServiceCollection();

            // se ejecuta el STARTUP de la aplicación .NETCORE
            var startup = new Startup(_configuration, Mock.Of<IWebHostEnvironment>(w =>
                 w.EnvironmentName == "Development" &&
                 w.ApplicationName == "NovaExpediente.WebUI"));

            // mock del hosting de la aplicación
            services.AddSingleton(Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "NovaExpediente.WebUI"));

            startup.ConfigureServices(services);

            // Reemplazo del servicio ICurrentUserService
            // para cambiarlo por un servicio mock
            var currentUserServiceDescriptor = services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            services.Remove(currentUserServiceDescriptor);

            services.AddScoped(provider =>
                Mock.Of<ICurrentUserService>(s => s.UserId == _currentUserId));

            // contexto de ejecución de los serivicios
            _scopeFactory = services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            // registro de EF Effort para evitar el uso de una base de datos real
            Effort.Provider.EffortProviderConfiguration.RegisterProvider();

            // se reemplazan las conexiones usadas para el aplicativo y manejo de usuarios
            AddContextInMemory(services);

            //_checkpoint = new Checkpoint
            //{
            //    TablesToIgnore = new[] { "__EFMigrationsHistory" }
            //};

            //EnsureDatabase();
        } 
        #endregion

        #region Reemplazo de contexto de BDs
        private void AddContextInMemory(ServiceCollection services)
        {
            AddNovaExpedienteDbContext(services);
            AddAppplicationDbContext(services);

            //services.AddDbContext<NovaExpedienteDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            //services.AddScoped<INovaExpedienteDbContext>(provider => provider.GetService<NovaExpedienteDbContext>());

            //var appDbContextDescriptor = services.FirstOrDefault(d =>
            //   d.ServiceType == typeof(ApplicationDbContext));

            //services.Remove(appDbContextDescriptor);

            ////var optionsAppDbContext = new DbContextOptionsBuilder<ApplicationDbContext>()
            ////    .UseInMemoryDatabase(Guid.NewGuid().ToString())
            ////    .EnableSensitiveDataLogging()
            ////    .Options;
            ////var contextAppDbContext = new ApplicationDbContext(optionsAppDbContext, null);
            ////AddMyData(context);
            //services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(Guid.NewGuid().ToString()));
            //services.AddScoped<ApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        }

        private static void AddNovaExpedienteDbContext(ServiceCollection services)
        {
            var contextDescriptor = services.FirstOrDefault(d =>
               d.ServiceType == typeof(NovaExpedienteDbContext));

            services.Remove(contextDescriptor);

            var connection = DbConnectionFactory.CreateTransient();

            services.AddDbContext<NovaExpedienteDbContext>(options =>
                options.UseSqlServer(connection));
            services.AddScoped<INovaExpedienteDbContext>(provider => provider.GetService<NovaExpedienteDbContext>());
        }

        private static void AddAppplicationDbContext(ServiceCollection services)
        {
            var contextDescriptor = services.FirstOrDefault(d =>
               d.ServiceType == typeof(ApplicationDbContext));

            services.Remove(contextDescriptor);

            var connection = DbConnectionFactory.CreateTransient();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connection));
            services.AddScoped<ApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        }
        #endregion

        #region Métodos en caso de usar una BD real para las pruebas
        private void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            context.Database.Migrate();

            var contextNovaExpediente = scope.ServiceProvider.GetService<NovaExpedienteDbContext>();

            contextNovaExpediente.Database.Migrate();
        }

        public static async Task ResetState()
        {
            try
            {
                //await _checkpoint.Reset(_configuration.GetConnectionString("NovaExpedienteDatabase"));
            }
            catch (Exception)
            {

                throw;
            }

            _currentUserId = null;
        }
        #endregion

        #region Métodos que simulan la creación de entidades e invocación de métodos de los API
        
        /// <summary>
        /// BUscar una entidad por ID
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<TEntity> FindAsync<TEntity>(int id)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<ApplicationDbContext>();

            return await context.FindAsync<TEntity>(id);
        }

        /// <summary>
        /// Agrega una entidad a un repositorio
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<NovaExpedienteDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Invocación de un método de un API
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        } 
        #endregion

        public static async Task<int?> RunAsDefaultUserAsync()
        {
            var userName = "test@local";
            var password = "Testing1234!";

            using var scope = _scopeFactory.CreateScope();

            var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser { UserName = userName, Email = userName };

            var result = await userManager.CreateAsync(user, password);

            _currentUserId = int.Parse(user.Id);

            return _currentUserId;
        }
    }
}
