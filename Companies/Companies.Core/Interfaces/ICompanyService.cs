namespace Companies.Core.Interfaces;

public interface ICompanyService
{
    Task<List<Company>> GetAll();
}