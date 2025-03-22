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
}