namespace Companies.Tests.Unit.Services;

public class CompanyServiceTests
{
    private readonly Mock<ICompanyRepository> _repository = new();
    private readonly Mock<IValidationService<Company>> _validationService = new();
    private readonly CompanyService _service;

    public CompanyServiceTests()
    {
        _service = new CompanyService(_repository.Object, _validationService.Object);
    }

    [Fact]
    public async Task ShouldCallRepositoryForGetAllAndReturnResults()
    {
        var company1 = new Company
        {
            Id = Guid.NewGuid(),
            Name = "ACME Corp",
            Exchange = "NYSE",
            Ticker = "ACME",
            Isin = "US12345678"
        };

        var company2 = new Company
        {
            Id = Guid.NewGuid(),
            Name = "Beta Corp",
            Exchange = "NASDAQ",
            Ticker = "BETA",
            Isin = "US87654321"
        };

        _repository.Setup(x => x.GetAll()).ReturnsAsync([company1, company2]);

        var results = await _service.GetAll();

        Assert.Equal([company1, company2], results);
    }


    [Fact]
    public async Task FailedValidationShouldReturnError()
    {
        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = "ACME Corp",
            Exchange = "NYSE",
            Ticker = "ACME",
            Isin = "US12345678"
        };

        _validationService.Setup(x => x.ValidateOnSave(company)).ReturnsAsync(new Result(false, "some error"));

        var result = await _service.SaveCompany(company);

        Assert.False(result.Success);
        Assert.Equal("some error", result.ErrorMessage);
    }

    [Fact]
    public async Task FailedValidationShouldNotSaveCompany()
    {
        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = "ACME Corp",
            Exchange = "NYSE",
            Ticker = "ACME",
            Isin = "US12345678"
        };

        _validationService.Setup(x => x.ValidateOnSave(company)).ReturnsAsync(new Result(false, "some error"));

        _ = await _service.SaveCompany(company);

        _repository.Verify(x => x.Save(It.IsAny<Company>()), Times.Never());
    }

    [Fact]
    public async Task SuccessfulValidationShouldReturnOk()
    {
        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = "ACME Corp",
            Exchange = "NYSE",
            Ticker = "ACME",
            Isin = "US12345678"
        };

        _validationService.Setup(x => x.ValidateOnSave(company)).ReturnsAsync(new Result(true));

        var result = await _service.SaveCompany(company);

        Assert.True(result.Success);
    }

    [Fact]
    public async Task SuccessfulValidationShouldSaveCompany()
    {
        var company = new Company
        {
            Id = Guid.NewGuid(),
            Name = "ACME Corp",
            Exchange = "NYSE",
            Ticker = "ACME",
            Isin = "US12345678"
        };

        _validationService.Setup(x => x.ValidateOnSave(company)).ReturnsAsync(new Result(true));

        _ = await _service.SaveCompany(company);

        _repository.Verify(x => x.Save(company), Times.Once());
    }
}