using Domain.Common.Models;

namespace Domain.Dinner.ValueObjects;
public class DinnerId : ValueObject
{
    public Guid Value { get; private set; }

    private DinnerId(Guid value)
    {
        Value = value;
    }

    public static DinnerId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
