using RatGang.Server.Authentication.Extensions;
using RatGang.Server.Tasks.Extensions;
using RatGang.Server.Users.Extensions;

namespace RatGang.Server.Application.ProjectOptions
{
    public static class MicroserviceOptions
    {
        public static IServiceCollection AddMicroservices(this WebApplicationBuilder builder)
        {
            //builder.Services.AddMicroservice(builder.Configuration);
            builder.Services.AddJwtBearerAuthentication(builder.Configuration);
            builder.Services.AddUsers(builder.Configuration);
            builder.Services.AddTasks(builder.Configuration);

            return builder.Services;
        }

        public static IServiceCollection AddGetOptionsMicroservices(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                //options.AddMicroservice();
                options.AddUsers();
                options.AddTasks();
                options.AddJwtBearerAuthentication();
            });
            return services;
        }

        public static WebApplication UseSwaggerUIMicroservices(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                //options.AddMicroservice();
                options.AddJwtBearerAuthentication();
                options.AddUsers();
                options.AddTasks();
            });

            //app.UseMicroservice();
            app.UseJwtBearerAuthentication();
            app.UseUsers();
            app.UseTasks();

            return app;
        }
    }
}
