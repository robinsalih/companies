namespace Companies.Core.Model;

public class User
{
    [Key]
    public required Guid Id { get; init; }
    public required string UserName { get; set; }
    public required string HashedPassword { get; set; }
    public required string Salt { get; set; }
    public required bool Enabled { get; set; }
    public required UserType Type { get; set; }
}