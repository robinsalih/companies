namespace Companies.Core.Model;

public class Company
{
    [Key]
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    [MaxLength(50)]
    public required string Exchange { get; init; }
    [MaxLength(20)]
    public required string Ticker { get; init; }
    [MaxLength(12)]
    public required string Isin { get; init; }
    public string? Website { get; init; }

}