namespace Companies.Infrastructure.Persistence;

public class CompanyRepository(CompanyContext context) : ICompanyRepository
{
    public Task<List<Company>> GetAll() => context.Companies.ToListAsync();
    public Task<Company?> GetById(Guid id) => context.Companies.FirstOrDefaultAsync(c => c.Id == id);
    public Task<Company?> GetByIsin(string isin) => context.Companies.FirstOrDefaultAsync(c => c.Isin == isin);
    public Task Save(Company company)
    {
        context.Companies.Add(company);
        return context.SaveChangesAsync();
    }

}