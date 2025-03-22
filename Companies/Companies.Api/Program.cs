namespace Companies.Api;

public class Program
{
    public static void Main(string[] args)
    {
        Setup(args).Build().Run();
    }

    public static IHostBuilder Setup(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });
}