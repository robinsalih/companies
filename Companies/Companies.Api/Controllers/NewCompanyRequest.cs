namespace Companies.Api.Controllers;

public class CompanyRequest
{
    public required string Name { get; set; }
    public required string Exchange { get; set; }
    public required string Ticker { get; set; }
    public required string Isin { get; set; }
    public string? Website { get; set; }
}