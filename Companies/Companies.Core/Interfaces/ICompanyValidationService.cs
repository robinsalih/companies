﻿namespace Companies.Core.Interfaces;

public interface ICompanyValidationService
{
    Task<Result> ValidateOnSave(Company company);
    Task<Result> ValidateOnUpdate(Company company);
}