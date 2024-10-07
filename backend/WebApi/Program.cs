using System.Net.Mime;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.HttpLogging;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureHttpJsonOptions(options => options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));

SetupLogging(builder);

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

Log.Information("Application started");

app.Run();

return;

static void SetupLogging(WebApplicationBuilder builder)
{
    Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

    builder.Host.UseSerilog();

    builder.Services.AddLogging(static builder =>
    {
        builder.ClearProviders();
        builder.AddSerilog(dispose: true);
    });

    builder.Services.AddHttpLogging(static options =>
    {
        options.LoggingFields = HttpLoggingFields.RequestMethod | HttpLoggingFields.Response;
        options.MediaTypeOptions.AddText(MediaTypeNames.Application.Json);
    });
}