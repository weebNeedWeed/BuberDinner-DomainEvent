using Application.Authentication.Commands.Register;
using Application.Authentication.Common;
using Application.Authentication.Queries.Login;
using Contracts.Authentication;
using Mapster;

namespace Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterRequest, RegisterCommand>()
            .Map(dest => dest, src => src);

        config.NewConfig<LoginRequest, LoginQuery>()
            .Map(dest => dest, src => src);

        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest, src => src.User)
            .Map(dest => dest, src => src);
    }
}
