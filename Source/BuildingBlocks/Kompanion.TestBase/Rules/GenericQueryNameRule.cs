using Mono.Cecil;
using NetArchTest.Rules;

namespace Kompanion.TestBase.Rules;

public class GenericQueryNameRule : ICustomRule
{
    public bool MeetsRule(TypeDefinition type)
    {
        ArgumentNullException.ThrowIfNull(type);

        string? className = GetClassName(type);

        return className?.EndsWith("Query", StringComparison.Ordinal) ?? false;
    }

    private static string? GetClassName(TypeDefinition type)
    {
        if (type.HasGenericParameters)
        {
            return type.Name.Split('`').FirstOrDefault();
        }

        return type.Name;
    }
}

