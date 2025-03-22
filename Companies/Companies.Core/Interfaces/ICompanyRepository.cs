namespace Companies.Core.Interfaces;

public interface ICompanyRepository
{
    Task<List<Company>> GetAll();
    Task Save(Company company);
    Task<Company?> GetById(Guid id);
    Task<Company?> GetByIsin(string isin);
}