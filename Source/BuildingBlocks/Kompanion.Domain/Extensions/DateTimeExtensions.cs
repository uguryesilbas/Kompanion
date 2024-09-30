namespace Kompanion.Domain.Extensions;

public static class DateTimeExtensions
{
    public static DateTime Universal => DateTime.Now.ToUniversalTime();
    public static DateTime Now => DateTime.Now;
    public static DateTime Utc => DateTime.UtcNow;
}