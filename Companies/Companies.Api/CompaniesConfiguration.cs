namespace Companies.Api;

public class CompaniesConfiguration(IConfiguration configuration) : IDatabaseConfiguration
{
    public string ConnectionString { get; } = configuration["DatabaseConnectionString"] ?? "";
}