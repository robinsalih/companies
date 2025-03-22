namespace Companies.Infrastructure.Persistence;

public interface IMigrationService
{
    Task MigrateDatabase();
}