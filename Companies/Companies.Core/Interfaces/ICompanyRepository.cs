using Companies.Core.Model;

namespace Companies.Core.Interfaces;

public interface ICompanyRepository
{
    Task<List<Company>> GetAll();
}