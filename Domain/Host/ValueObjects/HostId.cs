using Domain.Common.Models;

namespace Domain.Host.ValueObjects;
public class HostId : ValueObject
{
    public Guid Value { get; private set; }

    private HostId(Guid value)
    {
        Value = value;
    }

    public static HostId Create(string value)
    {
        return new(new Guid(value));
    }

    public static HostId Create(Guid value)
    {
        return new(value);
    }

    public static HostId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
