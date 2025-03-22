namespace Companies.Api;

public class Program
{
    public static async Task Main(string[] args)
    {
        var host = Setup(args).Build();
        using (var scope = host.Services.CreateScope())
        {
            var migrationService = scope.ServiceProvider.GetRequiredService<IMigrationService>();
            await migrationService.MigrateDatabase();
        }
        await host.RunAsync();
    }

    public static IHostBuilder Setup(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}