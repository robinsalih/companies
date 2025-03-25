namespace Companies.Api;

public class CompaniesConfiguration(IConfiguration configuration) : IDatabaseConfiguration
{
    public string DatabaseConnectionString { get; } = configuration.GetConnectionString("Companies") ?? "";
   
    public bool MigrateDatabaseOnStartup { get; } = bool.Parse(configuration["MigrateDatabaseOnStartup"] ?? "false");
}