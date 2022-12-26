using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;
using ProfilesManager.Domain.IRepositories;
using ProfilesManager.Persistence.DapperImplementation;
using ProfilesManager.Persistence.IDapperImplementation;
using ProfilesManager.Presentation.Validators;
using ProfilesManager.Service.Services;
using ProfilesManager.Services.Abstraction.IServices;

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
            string connectionStr = configuration.GetConnectionString("sqlConnection");

            services.AddScoped<IRepositoryManager, RepositoryManager>(
                provider => new RepositoryManager(connectionStr));
            services.AddScoped<ITablesManager, TablesManager>(
                provider => new TablesManager(connectionStr));
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddScoped<IServiceManager, ServiceManager>();
            services.AddScoped<IMigrationsService, MigrationsSevice>();
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<DoctorForRequestValidator>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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
