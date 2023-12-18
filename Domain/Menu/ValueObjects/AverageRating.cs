using Domain.Common.Models;

namespace Domain.Menu.ValueObjects;
public class AverageRating : ValueObject
{
    public double Value { get; private set; }
    public int NumRatings { get; private set; }

    private AverageRating(double value, int numRatings)
    {
        Value = value;
        NumRatings = numRatings;
    }

    public static AverageRating Create(double value, int numRatings)
    {
        return new(value, numRatings);
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
        yield return NumRatings;
    }
}
