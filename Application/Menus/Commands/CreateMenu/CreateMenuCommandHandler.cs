using Application.Common.Interfaces.Persistence;
using Domain.Host.ValueObjects;
using Domain.Menu;
using Domain.Menu.Entities;
using ErrorOr;
using MediatR;

namespace Application.Menus.Commands.CreateMenu;

public class CreateMenuCommandHandler : IRequestHandler<CreateMenuCommand, ErrorOr<Menu>>
{
    private readonly IMenuRepository _menuRepository;

    public CreateMenuCommandHandler(IMenuRepository menuRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<ErrorOr<Menu>> Handle(CreateMenuCommand command, CancellationToken cancellationToken)
    {
        await Task.Yield();

        var menu = Menu.Create(
            name: command.Name,
            description: command.Description,
            hostId: HostId.Create(command.HostId),
            menuSections: command.Sections.ConvertAll(x =>
                MenuSection.Create(
                    x.Name,
                    x.Description,
                    x.Items.ConvertAll(y => 
                        MenuItem.Create(y.Name, y.Description)))
            ));

        _menuRepository.Add(menu);

        return menu;
    }
}
