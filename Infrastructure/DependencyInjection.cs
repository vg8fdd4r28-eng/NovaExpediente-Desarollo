using System.Collections.Generic;
using System.Security.Claims;
using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Common;
using NovaExpediente.Infrastructure.Identity;
using NovaExpediente.Infrastructure.Curriculum;
using NovaExpediente.Infrastructure.Security;
using NovaExpediente.Infrastructure.CloudService;
using NovaExpediente.Infrastructure.Conversacion;
using NovaExpediente.Infrastructure.CurriculumVitae;
using NovaExpediente.Infrastructure.Informes;

namespace NovaExpediente.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
        {
            services.AddTransient<IOneSignalService, OneSignalService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<INominaService, NominaService>();
            services.AddTransient<ICurriculumService, CurriculumService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddScoped<IUserManager, UserManagerService>();
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IDateTime, MachineDateTime>();
            services.AddTransient<IConversacionService, ConversacionService>();
            services.AddTransient<ICurriculumVitaeService, CurriculumVitaeService>();
            services.AddTransient<IInformeService, InformeService>();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("NovaExpedienteDatabase")));

            services.AddDefaultIdentity<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            if (environment.IsEnvironment("Test"))
            {
                services.AddIdentityServer()
                    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>(options =>
                    {
                        options.Clients.Add(new Client
                        {
                            ClientId = "NovaExpediente.IntegrationTests",
                            AllowedGrantTypes = { GrantType.ResourceOwnerPassword },
                            ClientSecrets = { new Secret("secret".Sha256()) },
                            AllowedScopes = { "NovaExpediente.WebUIAPI", "openid", "profile" }
                        });
                    }).AddTestUsers(new List<TestUser>
                    {
                        new TestUser
                        {
                            SubjectId = "f26da293-02fb-4c90-be75-e4aa51e0bb17",
                            Username = "jason@northwind",
                            Password = "Northwind1!",
                            Claims = new List<Claim>
                            {
                                new Claim(JwtClaimTypes.Email, "jason@northwind")
                            }
                        }
                    });
            }
            else
            {
                services.AddIdentityServer()
                    .AddApiAuthorization<ApplicationUser, ApplicationDbContext>();
            }

            services.AddAuthentication()
                .AddIdentityServerJwt();

            return services;
        }
    }
}
