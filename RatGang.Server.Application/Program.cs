using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpOverrides;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RatGang.Server.Application.ProjectOptions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
{
    builder.AllowAnyHeader()
           .AllowAnyMethod()
           .AllowAnyOrigin();
}));
builder.Services.AddMemoryCache();

builder.Services.AddBaseJsonOptions();
builder.AddMicroservices();
builder.Services.AddGetOptionsMicroservices();

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

app.MapControllers();
app.UseCors("CorsPolicy");

app.UseSwaggerUIMicroservices();

app.Run();