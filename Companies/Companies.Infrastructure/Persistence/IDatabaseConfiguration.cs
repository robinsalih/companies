namespace Companies.Infrastructure.Persistence;

public interface IDatabaseConfiguration
{
    string ConnectionString { get; }
}