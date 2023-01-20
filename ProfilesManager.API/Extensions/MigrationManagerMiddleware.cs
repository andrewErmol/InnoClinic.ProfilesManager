using FluentMigrator.Runner;
using ProfilesManager.Persistence.Migrations;

namespace ProfilesManager.API.Extensions
{
    public static class MigrationManagerMiddleware
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var databaseService = scope.ServiceProvider.GetRequiredService<Database>();
                var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
                try
                {
                    databaseService.CreateDatabase("ProfilesDb");

                    migrationService.ListMigrations();
                    migrationService.MigrateUp();
                }
                catch
                {
                    throw;
                }
            }
            return host;
        }
    }
}
