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

    public async Task Update(Company company)
    {
        var oldRecord = await context.Companies.SingleAsync(c => c.Id == company.Id);
        
        oldRecord.Name = company.Name;
        oldRecord.Exchange = company.Exchange;
        oldRecord.Ticker = company.Ticker;
        oldRecord.Isin = company.Isin;
        oldRecord.Website = company.Website;
        
        await context.SaveChangesAsync();
    }

}