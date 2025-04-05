using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using RatGang.Server.Users.Database;
using RatGang.Server.Users.Services;
using RatGang.Server.Users.Services.Runtime;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RatGang.Server.Users.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddUsers(this IServiceCollection services, IConfiguration configuration)
    {
        Console.WriteLine("Build version <RG.Server.Users> V1.0.0");
        Configurate.Singleton = configuration.Get<Configurate>()!;
        Configurate.Singleton = configuration.GetSection("Modules:Users").Get<Configurate>() ?? new Configurate();
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(Configurate.Singleton, Newtonsoft.Json.Formatting.Indented));
        Console.WriteLine("--------------------");

        services.Database();
        services.Services();

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

    private static void Services(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IVerifyService, VerifyService>();
    }

    public static SwaggerGenOptions AddUsers(this SwaggerGenOptions options)
    {
        options.SwaggerDoc(name: "Users", new OpenApiInfo { Title = "Users", Version = "Users" });
        return options;
    }

    public static SwaggerUIOptions AddUsers(this SwaggerUIOptions options)
    {
        options.SwaggerEndpoint(url: $"/swagger/Users/swagger.json", name: "Users");
        return options;
    }

    public static WebApplication UseUsers(this WebApplication builder)
    {
        return builder;
    }
}
