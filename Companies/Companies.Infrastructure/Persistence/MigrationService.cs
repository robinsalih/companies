
namespace Companies.Infrastructure.Persistence;

public class MigrationService(CompanyContext context, IDatabaseConfiguration databaseConfiguration, ILogger logger) : IMigrationService
{
    public Task MigrateDatabase()
    {
        if (databaseConfiguration.MigrateDatabaseOnStartup)
        {
            logger.Information("MigrateDatabaseOnStartup enabled - running EF migration");
            return context.Database.MigrateAsync();
        }

        return Task.CompletedTask;      
    }
}