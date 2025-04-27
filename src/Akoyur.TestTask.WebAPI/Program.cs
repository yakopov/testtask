using Akoyur.TestTask.ApiModels.Validators;
using Akoyur.TestTask.Application.DI;
using Akoyur.TestTask.WebAPI.Filters;
using Akoyur.TestTask.WebAPI.Middlewares;
using Asp.Versioning;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Configuration;

namespace Akoyur.TestTask.WebAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add controllers for API endpoints
        builder.Services.AddControllers();

        // Add custom validation filter globally
        builder.Services.AddControllers(options =>
        {
            options.Filters.Add<ValidationFilter>();
        });

        // Suppress automatic model state invalid filter to use custom validation filter
        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        // Add FluentValidation for automatic model validation
        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining(typeof(ValidatorsAssembly));

        // API versioning setup
        builder.Services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        // Set up Swagger for API documentation and versioning
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            options.DocInclusionPredicate((version, apiDescription) =>
            {
                var versions = apiDescription.ActionDescriptor.EndpointMetadata
                    .OfType<ApiVersionAttribute>()
                    .SelectMany(attr => attr.Versions);

                return versions.Any(v => $"v{v}" == version); // Match versions (e.g., v1, v2)
            });

            options.SwaggerDoc("v1", new OpenApiInfo { Title = "API v1", Version = "v1" });
        });

        // Configure Serilog for logging
        builder.Host.UseSerilog((context, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext();
        });

        // CORS configuration to allow all origins, methods, and headers
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        // Add configuration settings from JSON files
        builder.Configuration
            .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
            .AddJsonFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "appsettings.Development.json"), optional: true, reloadOnChange: true);

        // Add application-specific services, including dependency injection for Mediator, etc.
        builder.Services.AddApplication(builder.Configuration);

        // Build the application
        var app = builder.Build();

        // Set up Swagger for development environment
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
            });
        }

        // Apply middleware for CORS, error handling, and authorization
        app.UseCors("AllowAll");
        app.UseMiddleware<ErrorHandlingMiddleware>();
        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        // Run the web application
        app.Run();
    }
}