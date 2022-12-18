using ProfilesManager.API.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.Enrich.FromLogContext()
        .ReadFrom.Configuration(context.Configuration);
});

// Add services to the container.

builder.Services.AddControllers()
    .AddApplicationPart(typeof(ProfilesManager.Presentation.AssemblyReference).Assembly);

builder.Services.ConfigureCors();

builder.Services.ConfigureDbManagers(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthentication();

builder.Services.ConfigureSwagger();

builder.Services.ConfigureServices();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware(typeof(ExceptionHandlerMiddleware));

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors("CorsPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();