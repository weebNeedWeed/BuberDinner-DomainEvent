using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Common.Interfaces.Services;
using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Interceptors;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastucture(
        this IServiceCollection services, IConfigurationManager configurationManager)
    {
        services.AddAuth(configurationManager);
        services.AddSingleton<IDateTimeProvider, DateTimeProvicer>();
        services.AddPersistence();

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<BuberDinnerDbContext>(options => 
            options.UseSqlServer(connectionString: "Server=localhost;Database=BuberDinner;User Id=sa;Password=@Admin123;Encrypt=True;TrustServerCertificate=True"));

        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddScoped<IMenuRepository, MenuRepository>();
        services.AddScoped<PublishDomainEventsInterceptor>();

        return services;
    }

    public static IServiceCollection AddAuth(
    this IServiceCollection services, IConfigurationManager configurationManager)
    {
        var jwtSettings = new JwtSettings();
        configurationManager.GetSection(JwtSettings.SectionName).Bind(jwtSettings);

        services.AddSingleton(Options.Create(jwtSettings));
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings.Issuer,
                ValidAudience = jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            });

        return services;
    }
}
