namespace Companies.Tests.Unit.Validators;

public class CompanyIsinValidValidatorTests
{
    [Fact]
    public async Task ShouldReturnOkForValidIsin()
    {
        var validator = new CompanyIsinValidValidator();
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

    [Theory]
    [InlineData("US037833100514")]
    [InlineData("US0378")]
    public async Task ShouldReturnErrorIfIsinWrongLength(string isin)
    {
        var validator = new CompanyIsinValidValidator();
        var company = new Company
        {
            Isin = isin,
            Id = Guid.NewGuid(),
            Name = "Name",
            Exchange = "Exchange",
            Ticker = "Ticker"
        };


        var result = await validator.ValidateOnSave(company);

        Assert.False(result.Success);
        Assert.Equal("ISIN must be 12 characters long", result.ErrorMessage);
    }

    [Theory]
    [InlineData("037833100514")]
    [InlineData("I37833100514")]
    [InlineData("!r7833100514")]
    [InlineData("1E7833100514")]
    [InlineData("ie7833100514")]
    public async Task ShouldReturnErrorIfFirstTwoCharactersNotLetters(string isin)
    {
        var validator = new CompanyIsinValidValidator();
        var company = new Company
        {
            Isin = isin,
            Id = Guid.NewGuid(),
            Name = "Name",
            Exchange = "Exchange",
            Ticker = "Ticker"
        };


        var result = await validator.ValidateOnSave(company);

        Assert.False(result.Success);
        Assert.Equal("First two characters of ISIN must be letters", result.ErrorMessage);
    }
}