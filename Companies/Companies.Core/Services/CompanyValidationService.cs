using Serilog;

namespace Companies.Core.Services;

public class CompanyValidationService(IEnumerable<ICompanyValidator> validators, ILogger logger) : ICompanyValidationService
{
    private readonly List<ICompanyValidator> _validators = validators.ToList();

    public async Task<Result> ValidateOnSave(Company Company)
    {
        logger.Debug("Company {name} with id {id} being validated", Company.Name, Company.Id);

        foreach (var validator in _validators)
        {
            var result = await validator.ValidateOnSave(Company);
            if (!result.Success)
            {
                logger.Information("Company failed validation, id: {id}, name {name}. reason {reason}", Company.Id, Company.Name, result.ErrorMessage);
                return result;
            }
        }

        logger.Debug("Company {name} with id {id} is valid", Company.Name, Company.Id);
        return new Result(true);
    }
}