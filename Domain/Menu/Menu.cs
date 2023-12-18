using Domain.Common.Models;
using Domain.Dinner.ValueObjects;
using Domain.Host.ValueObjects;
using Domain.Menu.Entities;
using Domain.Menu.Events;
using Domain.Menu.ValueObjects;
using Domain.MenuReview.ValueObjects;
using System.Runtime.CompilerServices;

namespace Domain.Menu;
public class Menu : AggregateRoot<MenuId>
{
    private readonly List<MenuSection> _sections = new();
    private readonly List<DinnerId> _dinnerIds = new();
    private readonly List<MenuReviewId> _menuReviewIds = new();

    public string Name { get; private set; }
    public string Description { get; private set; }
    public AverageRating AverageRating { get; private set; }
    public HostId HostId { get; private set; }
    public DateTime CreatedDateTime { get; private set; }
    public DateTime UpdatedDateTime { get; private set; }

    public IReadOnlyList<MenuSection> Sections => _sections.AsReadOnly();
    public IReadOnlyList<DinnerId> DinnerIds => _dinnerIds.AsReadOnly();
    public IReadOnlyList<MenuReviewId> MenuReviewIds => _menuReviewIds.AsReadOnly();

#pragma warning disable CS8618
    private Menu() { }
#pragma warning restore CS8618

    private Menu(
        MenuId id,
        string name,
        string description,
        AverageRating averageRating,
        HostId hostId,
        DateTime createdDateTime,
        DateTime updatedDateTime,
        List<MenuSection> menuSections) : base(id)
    {
        Name = name;
        Description = description;
        AverageRating = averageRating;
        HostId = hostId;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;

        _sections.AddRange(menuSections);
    }

    public static Menu Create(
        string name,
        string description,
        HostId hostId,
        List<MenuSection> menuSections)
    {
        var menu = new Menu(
            MenuId.CreateUnique(),
            name,
            description,
            AverageRating.Create(0, 0),
            hostId,
            DateTime.UtcNow,
            DateTime.UtcNow,
            menuSections);

        menu.AddDomainEvent(
            new MenuCreated(menu));

        return menu;
    }
}
