﻿namespace Companies.Core.Services;

public class CompanyService(ICompanyRepository repository, IValidationService<Company> validationService) : ICompanyService
{
    public Task<List<Company>> GetAll() => repository.GetAll();
    public async Task<Result> SaveCompany(Company company)
    {
        var validationResult = await validationService.ValidateOnSave(company);
        if (validationResult.Success)
            await repository.Save(company);

        return validationResult;
    }

    public Task<Company?> GetById(Guid id) => repository.GetById(id);

    public Task<Company?> GetByIsin(string isin) => repository.GetByIsin(isin);
    public async Task<Result> UpdateCompany(Company company)
    {
        var validationResult = await validationService.ValidateOnUpdate(company);
        if (validationResult.Success)
            await repository.Update(company);

        return validationResult;
    }
}