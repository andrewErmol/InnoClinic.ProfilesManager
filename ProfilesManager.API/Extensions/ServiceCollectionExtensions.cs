using FluentMigrator.Runner;
using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.OpenApi.Models;
using ProfilesManager.Domain.IRepositories;
using ProfilesManager.Messaging.Consumers;
using ProfilesManager.Persistence;
using ProfilesManager.Persistence.Migrations;
using ProfilesManager.Persistence.Repositories;
using ProfilesManager.Presentation.Validators;
using ProfilesManager.Service.Services;
using ProfilesManager.Services.Abstraction.IServices;
using ProfilesManager.Services.Services;
using System.Reflection;

namespace ProfilesManager.API.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(optionss =>
            {
                optionss.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            });

        public static void ConfigureDbManagers(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddSingleton<DapperContext>();
            services.AddSingleton<Database>();

            services.AddFluentMigratorCore()
                .ConfigureRunner(c => c.AddSqlServer2016()
                    .WithGlobalConnectionString(configuration.GetConnectionString("sqlConnection"))
                    .ScanIn(Assembly.GetAssembly(typeof(DapperContext))).For.Migrations());

            
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IRepositoryManager, RepositoryManager>(
                provider => new RepositoryManager(configuration.GetConnectionString("sqlConnection")));
            services.AddScoped<IPublishService, PublishService>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<DoctorForRequestValidator>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void ConfigureMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection("Messaging");

            services.AddMassTransit(x =>
            {
                x.AddConsumer<OfficeUpdatedConsumer>();
                x.AddConsumer<OfficeDeletedConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(config["Host"], "/", h => {
                        h.Username(config["UserName"]);
                        h.Password(config["Password"]);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                s.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer",
                            },
                            Name = "Bearer"
                        },
                        new List<string>()
                    }
                });
            });
        }
    }
}
