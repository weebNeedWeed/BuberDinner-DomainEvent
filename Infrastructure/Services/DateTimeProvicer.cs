using Application.Common.Interfaces.Services;

namespace Infrastructure.Services;
public class DateTimeProvicer : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
