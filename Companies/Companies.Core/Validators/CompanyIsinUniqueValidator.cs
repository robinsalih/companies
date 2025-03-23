namespace Companies.Core.Validators;

public class CompanyIsinUniqueValidator(ICompanyRepository repository) : IValidator<Company>
{
    public async Task<Result> ValidateOnSave(Company company)
    {
        var result = await repository.GetByIsin(company.Isin);
        return result != null
            ? new Result(false, "ISIN must be unique")
            : new Result(true);
    }

    public async Task<Result> ValidateOnUpdate(Company company)
    {
        var result = await repository.GetByIsin(company.Isin);
        return result != null && result.Id != company.Id
            ? new Result(false, "ISIN must be unique")
            : new Result(true);
    }
}
