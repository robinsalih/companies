namespace Companies.Core;

public static class ServiceManagerExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services) =>
        services.AddTransient<ICompanyValidationService, CompanyValidationService>()
            .AddTransient<ICompanyService, CompanyService>()
            .AddValidators();
    
    private static IServiceCollection AddValidators(this IServiceCollection services)
    {
        var validators = typeof(ServiceManagerExtensions).Assembly.GetTypes()
            .Where(t => !t.IsAbstract && t.IsClass &&
                        t.GetInterface(nameof(ICompanyValidator)) == typeof(ICompanyValidator));

        foreach (var validator in validators)
        {
            services.Add(new ServiceDescriptor(typeof(ICompanyValidator), validator, ServiceLifetime.Transient));
        }

        return services;
    }
}