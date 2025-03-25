using Dapper;

namespace Companies.Tests.Integration.BaseAndHelpers;

public class DatabaseHelper(string connectionString)
{
    public async Task<Company?> GetAndDeleteCompany(Guid id)
    {
        await using var connection = new SqlConnection(connectionString);
        var company = await connection.QueryFirstOrDefaultAsync<Company>("SELECT Id, Name, Exchange, Ticker, Isin, Website FROM Companies WHERE Id = @id", new { id });
        if (company != null)
            await connection.ExecuteAsync("DELETE Companies WHERE  Id = @id", new { id });

        return company;
    }
}