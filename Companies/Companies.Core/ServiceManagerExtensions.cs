namespace Companies.Core;

public static class ServiceManagerExtensions
{
    public static IServiceCollection AddCore(this IServiceCollection services) =>
        services.AddTransient<IValidationService<Company>, ValidationService<Company>>()
            .AddTransient<ICompanyService, CompanyService>()
            .AddValidators<Company>();
    
    private static IServiceCollection AddValidators<T>(this IServiceCollection services)
    {
        var validators = typeof(ServiceManagerExtensions).Assembly.GetTypes()
            .Where(t => t is { IsAbstract: false, IsClass: true }  && t.GetInterfaces()
                .Any(i => i.IsGenericType && typeof(IValidator<>) == i.GetGenericTypeDefinition() && i.GetGenericArguments()[0] == typeof(T)));

        foreach (var validator in validators)
        {
            services.Add(new ServiceDescriptor(typeof(IValidator<T>), validator, ServiceLifetime.Transient));
        }

        return services;
    }
}