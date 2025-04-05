using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RatGang.Server.Authentication.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddJwtBearerAuthentication(
            this IServiceCollection services, IConfiguration configuration)
        {
            Console.WriteLine("Build version <FG.Server.Authentication> V1.0.0");
            Configurate.Singleton = configuration.Get<Configurate>()!;
            Configurate.Singleton = configuration.GetSection("Modules:Authentication").Get<Configurate>() ?? new Configurate();
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Configurate.Singleton, Newtonsoft.Json.Formatting.Indented));
            Console.WriteLine("--------------------");

            services.AddJwtAuth();

            return services;
        }

        public static SwaggerGenOptions AddJwtBearerAuthentication(this SwaggerGenOptions options)
        {
            options.SwaggerDoc(name: "Authentication", new OpenApiInfo { Title = "Authentication", Version = "Authentication" });
            return options;
        }

        public static SwaggerUIOptions AddJwtBearerAuthentication(this SwaggerUIOptions options)
        {
            options.SwaggerEndpoint(url: $"/swagger/Authentication/swagger.json", name: "Authentication");
            return options;
        }

        public static WebApplication UseJwtBearerAuthentication(this WebApplication builder)
        {
            return builder;
        }

        public static IServiceCollection AddJwtAuth(this IServiceCollection services)
        {
            services.AddAuthentication()
                .AddJwtBearer("Access", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configurate.Singleton.Issuer,
                        ValidateAudience = true,
                        ValidAudience = Configurate.Singleton.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = Configurate.Singleton.GetSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                })
                .AddJwtBearer("Refresh", options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = Configurate.Singleton.Issuer,
                        ValidateAudience = true,
                        ValidAudience = Configurate.Singleton.Audience,
                        ValidateLifetime = true,
                        IssuerSigningKey = Configurate.Singleton.GetRefreshSymmetricSecurityKey(),
                        ValidateIssuerSigningKey = true,
                    };
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var accessToken = context.Request.Query["access_token"];

                            if (!string.IsNullOrEmpty(accessToken))
                            {
                                context.Token = accessToken;
                            }
                            return Task.CompletedTask;
                        }
                    };
                });
            return services;
        }
    }
}
