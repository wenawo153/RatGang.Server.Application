using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Minio;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RatGang.Server.Tasks.Database;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RatGang.Server.Tasks.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddTasks(this IServiceCollection services, IConfiguration configuration)
        {
            Console.WriteLine("Build version <RG.Server.Tasks> V1.0.0");
            Configurate.Singleton = configuration.Get<Configurate>()!;
            Configurate.Singleton = configuration.GetSection("Modules:Tasks").Get<Configurate>() ?? new Configurate();
            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Configurate.Singleton, Newtonsoft.Json.Formatting.Indented));
            Console.WriteLine("--------------------");

            services.Minio();
            services.Database();

            return services;
        }

        private static void Database(this IServiceCollection services)
        {
            services.AddDbContext<GeneralContext>(_ =>
            {
                _.UseNpgsql(Configurate.Singleton.DataBaseConnection);
                _.UseSnakeCaseNamingConvention();
            });
        }
        private static void Minio(this IServiceCollection services)
        {
            services.AddMinio(options => options
                .WithEndpoint(Configurate.Singleton.MinioOptions.Endpoint)
                .WithCredentials(
                    Configurate.Singleton.MinioOptions.AccessKey,
                    Configurate.Singleton.MinioOptions.SecretKey)
                .WithSSL(false)
                .Build());
        }

        public static SwaggerGenOptions AddTasks(this SwaggerGenOptions options)
        {
            options.SwaggerDoc(name: "Tasks", new OpenApiInfo { Title = "Tasks", Version = "Tasks" });
            return options;
        }

        public static SwaggerUIOptions AddTasks(this SwaggerUIOptions options)
        {
            options.SwaggerEndpoint(url: $"/swagger/Tasks/swagger.json", name: "Tasks");
            return options;
        }

        public static WebApplication UseTasks(this WebApplication builder)
        {
            return builder;
        }
    }
}
