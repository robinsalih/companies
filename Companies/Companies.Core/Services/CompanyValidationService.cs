using Serilog;

namespace Companies.Core.Services;

public class CompanyValidationService(IEnumerable<ICompanyValidator> validators, ILogger logger) : ICompanyValidationService
{
    private readonly List<ICompanyValidator> _validators = validators.ToList();

    public Task<Result> ValidateOnSave(Company company) => Validate(company, validator => validator.ValidateOnSave(company));
    public Task<Result> ValidateOnUpdate(Company company) => Validate(company, validator => validator.ValidateOnUpdate(company));

    private async Task<Result> Validate(Company company, Func<ICompanyValidator, Task<Result>> validationMethod)
    {
        logger.Debug("company {name} with id {id} being validated", company.Name, company.Id);

        foreach (var validator in _validators)
        {
            var result = await validationMethod(validator);
            if (!result.Success)
            {
                logger.Information("company failed validation, id: {id}, name {name}. reason {reason}", company.Id, company.Name, result.ErrorMessage);
                return result;
            }
        }

        logger.Debug("company {name} with id {id} is valid", company.Name, company.Id);
        return new Result(true);
    }
}