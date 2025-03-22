namespace Companies.Tests.Unit.Services;

public class CompanyValidationServiceTests
{
    private readonly ILogger _logger = new Mock<ILogger>().Object;
    private readonly Mock<ICompanyValidator> _firstValidator = new();
    private readonly Mock<ICompanyValidator> _secondValidator = new();
    private readonly CompanyValidationService _service;

    public CompanyValidationServiceTests()
    {
        _service = new CompanyValidationService([_firstValidator.Object, _secondValidator.Object], _logger);
    }

    [Fact]
    public async Task ShouldReturnOkWhenAllValidatorsPass()
    {
        var company = new Company
        {
            Isin = "US0378331005",
            Id = Guid.NewGuid(),
            Name = "Name",
            Exchange = "Exchange",
            Ticker = "Ticker"
        };

        _firstValidator.Setup(x => x.ValidateOnSave(company)).ReturnsAsync(new Result(true));
        _secondValidator.Setup(x => x.ValidateOnSave(company)).ReturnsAsync(new Result(true));

        var result = await _service.ValidateOnSave(company);

        Assert.True(result.Success);
        _firstValidator.Verify(x => x.ValidateOnSave(company), Times.Once);
        _secondValidator.Verify(x => x.ValidateOnSave(company), Times.Once);
    }


    [Fact]
    public async Task ShouldFailOnFirstValidation()
    {
        var company = new Company
        {
            Isin = "US0378331005",
            Id = Guid.NewGuid(),
            Name = "Name",
            Exchange = "Exchange",
            Ticker = "Ticker"
        };

        _firstValidator.Setup(x => x.ValidateOnSave(company)).ReturnsAsync(new Result(false, "Bad Company"));
        _secondValidator.Setup(x => x.ValidateOnSave(company)).ReturnsAsync(new Result(true));

        var result = await _service.ValidateOnSave(company);

        Assert.False(result.Success);
        Assert.Equal("Bad Company", result.ErrorMessage);
        _firstValidator.Verify(x => x.ValidateOnSave(company), Times.Once);
        _secondValidator.Verify(x => x.ValidateOnSave(company), Times.Never);
    }

    [Fact]
    public async Task ShouldFailOnSecondValidation()
    {
        var company = new Company
        {
            Isin = "US0378331005",
            Id = Guid.NewGuid(),
            Name = "Name",
            Exchange = "Exchange",
            Ticker = "Ticker"
        };

        _firstValidator.Setup(x => x.ValidateOnSave(company)).ReturnsAsync(new Result(true));
        _secondValidator.Setup(x => x.ValidateOnSave(company)).ReturnsAsync(new Result(false, "Bad Company"));

        var result = await _service.ValidateOnSave(company);

        Assert.False(result.Success);
        Assert.Equal("Bad Company", result.ErrorMessage);
        _firstValidator.Verify(x => x.ValidateOnSave(company), Times.Once);
        _secondValidator.Verify(x => x.ValidateOnSave(company), Times.Once);
    }
}