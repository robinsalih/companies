namespace Companies.Infrastructure.Persistence;

public class CompanyRepository(CompanyContext context) : ICompanyRepository
{
    public Task<List<Company>> GetAll() => context.Companies.ToListAsync();
    public Task Save(Company company)
    {
        context.Companies.Add(company);
        return context.SaveChangesAsync();
    }
}