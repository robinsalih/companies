namespace Companies.Core.Validators;

public class CompanyIsinValidValidator : IValidator<Company>
{
    public Task<Result> ValidateOnSave(Company company) => Validate(company);
    public Task<Result> ValidateOnUpdate(Company company) => Validate(company);

    private Task<Result> Validate(Company company)
    {
        if (company.Isin.Length != 12)
            return Task.FromResult(new Result(false, "ISIN must be 12 characters long"));

        if (!IsAtoZ(company.Isin[0]) || !IsAtoZ(company.Isin[1]))
            return Task.FromResult(new Result(false, "First two characters of ISIN must be letters"));

        return Task.FromResult(new Result(true));
    }

    private bool IsAtoZ(char c) => c is >= 'A' and <= 'Z';
}