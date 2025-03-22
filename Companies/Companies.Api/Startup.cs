using ILogger = Serilog.ILogger;

namespace Companies.Api;

public class Startup
{
    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        var logger = CreateSerilogLogger();
        var companiesConfiguration = new CompaniesConfiguration(configuration);

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        //services.AddCore();
        services.AddInfrastructure(companiesConfiguration);
        services.AddSingleton(logger);
    }

    private static ILogger CreateSerilogLogger()
    {
        // TODO: Implement using config file, could write to files, DataDog etc.  Can also add tags such as git commit, environment etc.

        return new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}