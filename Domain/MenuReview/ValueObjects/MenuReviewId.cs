using Domain.Common.Models;

namespace Domain.MenuReview.ValueObjects;

public class MenuReviewId : ValueObject
{
    public Guid Value { get; private set; }

    private MenuReviewId(Guid value)
    {
        Value = value;
    }

    public static MenuReviewId CreateUnique()
    {
        return new(Guid.NewGuid());
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
