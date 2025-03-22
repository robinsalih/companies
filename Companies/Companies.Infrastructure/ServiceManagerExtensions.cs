namespace Companies.Infrastructure;

public static class ServiceManagerExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IDatabaseConfiguration config) =>
        services.AddTransient<ICompanyRepository, CompanyRepository>()
                .AddDbContext<CompanyContext>(opt => opt.UseSqlServer(connectionString:config.ConnectionString));  
}