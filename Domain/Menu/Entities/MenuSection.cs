using Domain.Common.Models;
using Domain.Menu.ValueObjects;

namespace Domain.Menu.Entities;
public class MenuSection : Entity<MenuSectionId>
{
    private readonly List<MenuItem> _items = new();
    
    public string Name { get; private set; }
    public string Description { get; private set; }

    public IReadOnlyList<MenuItem> Items => _items.AsReadOnly();

#pragma warning disable CS8618
    private MenuSection() { }
#pragma warning restore CS8618

    private MenuSection(
        MenuSectionId id,
        string name,
        string description,
        List<MenuItem> menuItems) : base(id)
    {
        Name = name;
        Description = description;

        _items.AddRange(menuItems);
    }

    public static MenuSection Create(
        string name,
        string description, 
        List<MenuItem> menuItems)
    {
        return new(
            MenuSectionId.CreateUnique(),
            name,
            description,
            menuItems);
    }
}
