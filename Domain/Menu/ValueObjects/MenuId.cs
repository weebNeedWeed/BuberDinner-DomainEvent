using Domain.Common.Models;

namespace Domain.Menu.ValueObjects;
public class MenuId : ValueObject
{
    public Guid Value { get; private set; }

    private MenuId(Guid value) 
    {
        Value = value;
    }

    public static MenuId CreateUnique()
    {
        return new(Guid.NewGuid()); 
    }

    public static MenuId Create(Guid value)
    {
        return new(value);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
