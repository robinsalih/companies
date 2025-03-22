namespace Companies.Infrastructure.Persistence;

public interface IDatabaseConfiguration
{
    string DatabaseConnectionString { get; }
    bool MigrateDatabaseOnStartup { get; }
}