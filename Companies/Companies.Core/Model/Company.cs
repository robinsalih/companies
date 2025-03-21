namespace Companies.Core.Model;

public class Company
{
    [Key]
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Exchange { get; init; }
    public required string Ticker { get; init; }
    public string? Website { get; init; }

}