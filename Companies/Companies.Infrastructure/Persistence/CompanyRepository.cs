namespace Companies.Infrastructure.Persistence;

public class CompanyRepository(CompanyContext context) : ICompanyRepository
{
    public Task<List<Company>> GetAll() => context.Companies.ToListAsync();
}