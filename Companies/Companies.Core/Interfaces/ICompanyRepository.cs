namespace Companies.Core.Interfaces;

public interface ICompanyRepository
{
    Task<List<Company>> GetAll();
    Task Save(Company company);
}