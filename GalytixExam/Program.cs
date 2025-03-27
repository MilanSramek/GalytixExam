using Application;
using GalytixExam;
using Microsoft.OpenApi.Models;
using Persistence;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(config.GetValue<int>("Port"));
});

var services = builder.Services;
services
    .AddPersistence()
    .AddApplication()
    .AddPresentation();

services
    .AddOptions<CsvPremiumsRepositorySettings>()
    .Configure(options => options.Path = config.GetValue<string>("PremiumDataPath")!)
    .ValidateDataAnnotations();

services
    .AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "GalytixExam API",
            Version = "v1"
        });
    });

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "GalytixExam API v1");
    c.RoutePrefix = string.Empty;
});
app.MapControllers();
app.Run();