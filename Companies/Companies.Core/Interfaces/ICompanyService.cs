namespace Companies.Core.Interfaces;

public interface ICompanyService
{
    Task<List<Company>> GetAll();
    Task<Result> SaveCompany(Company company);
    Task<Company?> GetById(Guid id);
    Task<Company?> GetByIsin(string isin);
    Task<Result> UpdateCompany(Company company);
}