using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace RatGang.Server.Application.ProjectOptions
{
    public static class JsonOptions
    {
        public static IServiceCollection AddBaseJsonOptions(this IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Converters.Add(new StringEnumConverter
                    {
                        NamingStrategy = new CamelCaseNamingStrategy(),
                    });
                })
                .AddJsonOptions(_ =>
                {
                    _.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                    _.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                    _.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            return services;
        }
    }
}
