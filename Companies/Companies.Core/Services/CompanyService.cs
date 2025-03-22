namespace Companies.Core.Services;

public class CompanyService(ICompanyRepository repository) : ICompanyService
{
    public Task<List<Company>> GetAll() => repository.GetAll();
}