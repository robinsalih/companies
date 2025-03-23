namespace Companies.Core.Interfaces;

public interface IValidationService<in T>
{
    Task<Result> ValidateOnSave(T record);
    Task<Result> ValidateOnUpdate(T record);
} 