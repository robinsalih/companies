namespace Companies.Core.Interfaces;

public interface ICompanyValidator
{
    Task<Result> ValidateOnSave(Company company);
}