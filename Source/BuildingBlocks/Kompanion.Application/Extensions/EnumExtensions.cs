using System.ComponentModel;

namespace Kompanion.Application.Extensions;

public static class EnumExtensions
{
    public static string GetDescription<T>(this T value) where T : Enum
    {
        try
        {
            if (value.GetType().GetField(value.ToString())?.GetCustomAttributes(typeof(DescriptionAttribute), true) is DescriptionAttribute[] { Length: > 0 } attributes)
            {
                return attributes.First().Description;
            }
        }
        catch
        {
            // ignored
        }

        return string.Empty;
    }

    public static T ToEnum<T>(this string value)
    {
        return (T)Enum.Parse(typeof(T), value, true);
    }
}