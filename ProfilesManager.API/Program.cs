﻿using Microsoft.IdentityModel.Tokens;
using ProfilesManager.API.Extensions;
using ProfilesManager.Presentation.Controllers;
using Serilog;
using FluentMigrator.Runner;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.Enrich.FromLogContext()
        .ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(typeof(DoctorsController).Assembly);

builder.Services.ConfigureCors();

builder.Services.ConfigureDbManagers(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication("Bearer")
         .AddJwtBearer("Bearer", options =>
         {
             options.Authority = "https://localhost:7130";

             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateAudience = false
             };
         });

builder.Services.AddAuthentication();

builder.Services.ConfigureSwagger();

builder.Services.ConfigureServices(builder.Configuration);
builder.Services.ConfigureMassTransit(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MigrateDatabase();

app.MapControllers();

app.Run();