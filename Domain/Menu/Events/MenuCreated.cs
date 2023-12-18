using Domain.Common.Models;

namespace Domain.Menu.Events;
public record MenuCreated(Menu Menu) : IDomainEvent;
