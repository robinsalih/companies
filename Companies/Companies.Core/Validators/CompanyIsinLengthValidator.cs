namespace Companies.Core.Validators;

public class CompanyIsinValidValidator : ICompanyValidator
{
    public Task<Result> ValidateOnSave(Company company)
    {
        if (company.Isin.Length != 12)
            return Task.FromResult(new Result(false, "ISIN must be 12 characters long"));

        if (!IsAtoZ(company.Isin[0]) || !IsAtoZ(company.Isin[1]))
            return Task.FromResult(new Result(false, "First two characters of ISIN must be letters"));
        
        return Task.FromResult(new Result(true));
    }

    private bool IsAtoZ(char c) => c is >= 'A' and <= 'Z';
}