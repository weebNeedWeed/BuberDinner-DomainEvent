using Domain.Common.Models;

namespace Domain.Menu.ValueObjects;
public class MenuItemId : ValueObject
{
    public Guid Value { get; private set; }

    private MenuItemId(Guid value)
    {
        Value = value;
    }
    public static MenuItemId Create(Guid value)
    {
        return new(value);
    }


    public static MenuItemId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
