using Api.Common.Errors;
using Api.Common.Mapping;

namespace Api;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMappings();
        services.AddCustomProblemDetails();
        services.AddControllers();

        return services;
    }
}