using Domain.Common.Models;

namespace Domain.Menu.ValueObjects;
public class MenuSectionId : ValueObject
{
    public Guid Value { get; private set; }

    private MenuSectionId(Guid value)
    {
        Value = value;
    }

    public static MenuSectionId Create(Guid value)
    {
        return new(value);
    }

    public static MenuSectionId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
