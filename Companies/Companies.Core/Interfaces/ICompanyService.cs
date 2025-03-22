namespace Companies.Core.Interfaces;

public interface ICompanyService
{
    Task<List<Company>> GetAll();
    Task<Result> SaveCompany(Company company);
}