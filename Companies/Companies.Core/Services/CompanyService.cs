namespace Companies.Core.Services;

public class CompanyService(ICompanyRepository repository, ICompanyValidationService validationService) : ICompanyService
{
    public Task<List<Company>> GetAll() => repository.GetAll();
    public async Task<Result> SaveCompany(Company company)
    {
        var validationResult = await validationService.ValidateOnSave(company);
        if (validationResult.Success)
            await repository.Save(company);

        return validationResult;

    }

    public Task<Company?> GetById(Guid id) => repository.GetById(id);

    public Task<Company?> GetByIsin(string isin) => repository.GetByIsin(isin);
}