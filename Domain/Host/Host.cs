using Domain.Common.Models;
using Domain.Host.ValueObjects;
using Domain.Menu.ValueObjects;

namespace Domain.Host;
public sealed class Host : AggregateRoot<HostId>
{
    private readonly List<MenuId> _menuIds = new();

    public IReadOnlyList<MenuId> MenuIds => _menuIds.AsReadOnly();

    private Host(HostId id) : base(id) { }

#pragma warning disable CS8618
    private Host() { }
#pragma warning restore CS8618
}
