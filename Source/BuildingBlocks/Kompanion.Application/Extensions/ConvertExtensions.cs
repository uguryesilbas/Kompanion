namespace Kompanion.Application.Extensions;
public static class ConvertExtensions
{
    public static T As<T>(this object obj)
    {
        try
        {
            Type type = typeof(T);

            if (obj is not null)
            {
                if (!IsNullableType(type) && !string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    return (T)Convert.ChangeType(obj, type);
                }

                if (string.IsNullOrWhiteSpace(obj.ToString()))
                {
                    return default;
                }
            }

            return (T)Convert.ChangeType(obj, Nullable.GetUnderlyingType(type)!);
        }
        catch
        {
            return default;
        }
    }

    private static bool IsNullableType(Type type)
    {
        return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
    }
}