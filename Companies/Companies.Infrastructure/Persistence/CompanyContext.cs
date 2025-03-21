namespace Companies.Infrastructure.Persistence;

public class CompanyContext(DbContextOptions<CompanyContext> options)  : DbContext(options)
{
    public required DbSet<Company> Companies { get; set; }
}