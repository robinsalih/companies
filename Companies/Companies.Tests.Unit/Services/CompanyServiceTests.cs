namespace Companies.Tests.Unit.Services;

public class CompanyServiceTests
{
    private readonly Mock<ICompanyRepository> _repository = new();
    private readonly ICompanyService _service;

    public CompanyServiceTests()
    {
        _service = new CompanyService(_repository.Object);
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
}