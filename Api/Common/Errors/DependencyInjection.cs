using Api.Common.Http;
using ErrorOr;

namespace Api.Common.Errors;

public static class DependencyInjection
{
    public static IServiceCollection AddCustomProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails(options => 
            options.CustomizeProblemDetails = ctx =>
            {
                if(ctx.HttpContext.Items[HttpContextItemKeys.Errors] is List<Error> errors)
                {
                    ctx.ProblemDetails.Extensions["errorCodes"] = errors.ConvertAll(x => x.Code);
                }
            });
        return services;
    } 
}
