using Serilog;

namespace Companies.Core.Services;

public class ValidationService<T>(IEnumerable<IValidator<T>> validators, ILogger logger) : IValidationService<T>
{
    private readonly List<IValidator<T>> _validators = validators.ToList();

    public Task<Result> ValidateOnSave(T record) => Validate(record, validator => validator.ValidateOnSave(record));
    public Task<Result> ValidateOnUpdate(T record) => Validate(record, validator => validator.ValidateOnUpdate(record));

    private async Task<Result> Validate(T record, Func<IValidator<T>, Task<Result>> validationMethod)
    {
        logger.Debug("{type} {value} being validated.", nameof(T), record);

        foreach (var validator in _validators)
        {
            var result = await validationMethod(validator);
            if (!result.Success)
            {
                logger.Information("{type} {value} failed validation. Error: {error}", nameof(T), record, result.ErrorMessage);
                return result;
            }
        }

        logger.Debug("{type} {name} is valid", nameof(T), record);
        return new Result(true);
    }
}