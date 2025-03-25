namespace Companies.Tests.Integration.BaseAndHelpers;

public abstract class TestsBase
{
    private const string ConnectionString = "Server=localhost;User Id=sa;Password=Welcome1$;Database=Companies;TrustServerCertificate=true";
    protected readonly TestServer TestServer;
    protected readonly HttpClient TestClient;
    protected readonly DatabaseHelper Helper = new(ConnectionString);
    
    protected TestsBase()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .AddInMemoryCollection([new("ConnectionStrings:Companies", ConnectionString)]);

        TestServer = new TestServer(new WebHostBuilder()
            .UseStartup<Startup>()
            .UseConfiguration(configurationBuilder.Build()));
        
        TestClient = TestServer.CreateClient();
    }

    ~TestsBase()
    {
        TestServer.Dispose();
        TestClient.Dispose();
    }
}