using IdentityModel;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NovaExpediente.WebUI.Configuration;
using NovaExpediente.WebUI.Configuration.Constants;

namespace NovaExpediente.WebUI.Helpers
{
    public static class StartupHelpers
    {
       
        public static IServiceCollection AddNovaExpedienteApiCors(this IServiceCollection services, NovaExpedienteApiConfiguration vueApiConfiguration)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        if (vueApiConfiguration.CorsAllowAnyOrigin)
                        {
                            builder.AllowAnyOrigin();
                        }
                        else
                        {
                            builder.WithOrigins(vueApiConfiguration.CorsAllowOrigins);
                        }

                        builder.AllowAnyHeader();
                        builder.AllowAnyMethod();
                    });
            });

            return services;
        }

        /// <summary>
        /// Register services for MVC
        /// </summary>
        /// <param name="services"></param>
        

        

        /// <summary>
        /// Add authentication middleware for an API
        /// </summary>
        /// <typeparam name="TIdentityDbContext">DbContext for an access to Identity</typeparam>
        /// <typeparam name="TUser">Entity with User</typeparam>
        /// <typeparam name="TRole">Entity with Role</typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddApiAuthentication(this IServiceCollection services,
            IConfiguration configuration)
           
        {
            var vueApiConfiguration = configuration.GetSection(nameof(NovaExpedienteApiConfiguration)).Get<NovaExpedienteApiConfiguration>();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                options.DefaultForbidScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            })
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = vueApiConfiguration.IdentityServerBaseUrl;
                    options.ApiName = vueApiConfiguration.OidcApiName;
                    options.RequireHttpsMetadata = vueApiConfiguration.RequireHttpsMetadata;
                });
        }
        
        public static void AddForwardHeaders(this IApplicationBuilder app)
        {
            var forwardingOptions = new ForwardedHeadersOptions()
            {
                ForwardedHeaders = ForwardedHeaders.All
            };

            forwardingOptions.KnownNetworks.Clear();
            forwardingOptions.KnownProxies.Clear();

            app.UseForwardedHeaders(forwardingOptions);
        }

        public static void AddAuthorizationPolicies(this IServiceCollection services)
        {
            var vueApiConfiguration = services.BuildServiceProvider().GetService<NovaExpedienteApiConfiguration>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(AuthorizationConsts.AdministrationPolicy,
                    policy =>
                        policy.RequireAssertion(context => context.User.HasClaim(c =>
                                (c.Type == JwtClaimTypes.Role && c.Value == vueApiConfiguration.AdministrationRole) ||
                                (c.Type == $"client_{JwtClaimTypes.Role}" && c.Value == vueApiConfiguration.AdministrationRole)
                            )
                        ));
            });
        }
    }
}






