using Domain.Menu.Events;
using MediatR;

namespace Application.Hosts.Events;

public class MenuCreatedHandler : INotificationHandler<MenuCreated>
{
    public async Task Handle(MenuCreated notification, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }
}
