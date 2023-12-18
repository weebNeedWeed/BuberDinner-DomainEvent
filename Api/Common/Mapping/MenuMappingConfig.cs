using Application.Menus.Commands.CreateMenu;
using Contracts.Menus;
using Domain.Menu;
using Domain.Menu.Entities;
using Mapster;

namespace Api.Common.Mapping;

public class MenuMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<(CreateMenuRequest Request, string HostId), CreateMenuCommand>()
            .Map(dest => dest.HostId, src => src.HostId)
            .Map(dest => dest, src => src.Request);

        config.NewConfig<Menu, MenuResponse>()
            .Map(dest => dest.Id, src => src.Id.Value)
            .Map(dest => dest.HostId, src => src.HostId.Value)
            .Map(dest => dest.AverageRating, src => src.AverageRating.NumRatings > 0 ? src.AverageRating.Value : default)
            .Map(dest => dest.DinnerIds, src => src.DinnerIds.Select(x => x.Value))
            .Map(dest => dest.MenuReviewIds, src => src.MenuReviewIds.Select(x => x.Value));

        config.NewConfig<MenuSection, MenuSectionResponse>()
            .Map(dest => dest.Id, dest => dest.Id.Value);

        config.NewConfig<MenuItem, MenuItemResponse>()
            .Map(dest => dest.Id, dest => dest.Id.Value);
    }
}
