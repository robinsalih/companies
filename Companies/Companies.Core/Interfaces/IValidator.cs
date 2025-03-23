namespace Companies.Core.Interfaces;

public interface IValidator<in T>
{
    Task<Result> ValidateOnSave(T record);
    Task<Result> ValidateOnUpdate(T record);
}