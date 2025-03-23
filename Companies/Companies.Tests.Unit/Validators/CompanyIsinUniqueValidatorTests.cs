namespace Companies.Tests.Unit.Validators;

public class CompanyIsinUniqueValidatorTests
{
    [Fact]
    public async Task ShouldReturnOkForUniqueIsin()
    {
        var repository = new Mock<ICompanyRepository>();
        repository.Setup(x => x.GetByIsin("US0378331005")).ReturnsAsync((Company?)null);
        var validator = new CompanyIsinUniqueValidator(repository.Object);
        var company = new Company
        {
            Isin = "US0378331005",
            Id = Guid.NewGuid(),
            Name = "Name",
            Exchange = "Exchange",
            Ticker = "Ticker"
        };

        var result = await validator.ValidateOnSave(company);

        Assert.True(result.Success);
        Assert.Null(result.ErrorMessage);
    }

    [Fact]
    public async Task ShouldReturnErrorForNonUniqueIsin()
    {
        var existingCompany = new Company
        {
            Id = Guid.NewGuid(),
            Name = "ABC Corp",
            Exchange = "NYSE",
            Ticker = "ABC",
            Isin = "US0378331005"
        };

        var repository = new Mock<ICompanyRepository>();
        repository.Setup(x => x.GetByIsin(It.IsAny<string>())).ReturnsAsync(existingCompany);
        var validator = new CompanyIsinUniqueValidator(repository.Object);
        var company = new Company
        {
            Isin = "US0378331005",
            Id = Guid.NewGuid(),
            Name = "Name",
            Exchange = "Exchange",
            Ticker = "Ticker"
        };

        var result = await validator.ValidateOnSave(company);

        Assert.False(result.Success);
        Assert.Equal("ISIN must be unique", result.ErrorMessage);
    }

    [Fact]
    public async Task ShouldReturnOkForUniqueIsinOnUpdateWhenIsinUnchanged()
    {
        var companyOld = new Company
        {
            Isin = "US0378331005",
            Id = Guid.NewGuid(),
            Name = "Old Name",
            Exchange = "Exchange",
            Ticker = "Ticker"
        };

        var repository = new Mock<ICompanyRepository>();
        repository.Setup(x => x.GetByIsin(It.IsAny<string>())).ReturnsAsync(companyOld);
        var validator = new CompanyIsinUniqueValidator(repository.Object);
        var companyNew = new Company
        {
            Isin = "US0378331005",
            Id = companyOld.Id,
            Name = "New Name",
            Exchange = "Exchange",
            Ticker = "Ticker"
        };

        var result = await validator.ValidateOnUpdate(companyNew);

        Assert.True(result.Success);
        Assert.Null(result.ErrorMessage);
    }

    [Fact]
    public async Task ShouldReturnOkForUniqueIsinOnUpdate()
    {
        var repository = new Mock<ICompanyRepository>();
        repository.Setup(x => x.GetByIsin(It.IsAny<string>())).ReturnsAsync((Company?)null);
        var validator = new CompanyIsinUniqueValidator(repository.Object);
        var company = new Company
        {
            Isin = "US0378331005",
            Id = Guid.NewGuid(),
            Name = "Name",
            Exchange = "Exchange",
            Ticker = "Ticker"
        };

        var result = await validator.ValidateOnUpdate(company);

        Assert.True(result.Success);
        Assert.Null(result.ErrorMessage);
    }

    [Fact]
    public async Task ShouldReturnErrorForNonUniqueIsinOnUpdate()
    {
        var existingCompany = new Company
        {
            Id = Guid.NewGuid(),
            Name = "ABC Corp",
            Exchange = "NYSE",
            Ticker = "ABC",
            Isin = "US0378331005"
        };

        var repository = new Mock<ICompanyRepository>();
        repository.Setup(x => x.GetByIsin(It.IsAny<string>())).ReturnsAsync(existingCompany);
        var validator = new CompanyIsinUniqueValidator(repository.Object);
        var company = new Company
        {
            Isin = "US0378331005",
            Id = Guid.NewGuid(),
            Name = "Name",
            Exchange = "Exchange",
            Ticker = "Ticker"
        };

        var result = await validator.ValidateOnUpdate(company);

        Assert.False(result.Success);
        Assert.Equal("ISIN must be unique", result.ErrorMessage);
    }
}