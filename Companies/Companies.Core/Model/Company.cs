namespace Companies.Core.Model;

public class Company
{
    [Key]
    public required Guid Id { get; init; }
    public required string Name { get; set; }
    [MaxLength(50)]
    public required string Exchange { get; set; }
    [MaxLength(20)]
    public required string Ticker { get; set; }
    [MaxLength(12)]
    public required string Isin { get; set; }
    public string? Website { get; set; }

}