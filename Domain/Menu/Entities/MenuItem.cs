using Domain.Common.Models;
using Domain.Menu.ValueObjects;

namespace Domain.Menu.Entities;
public class MenuItem : Entity<MenuItemId>
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    private MenuItem(MenuItemId id,
                     string name,
                     string description) : base(id)
    {
        Name = name;
        Description = description;
    }

#pragma warning disable CS8618
    private MenuItem() { }
#pragma warning restore CS8618

    public static MenuItem Create(
        string name,
        string description)
    {
        return new(MenuItemId.CreateUnique(), name, description);
    }
}
