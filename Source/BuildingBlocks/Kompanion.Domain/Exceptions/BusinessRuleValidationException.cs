using Kompanion.Domain.Interfaces;

namespace Kompanion.Domain.Exceptions;

public class BusinessRuleValidationException : Exception
{
    public IBusinessRule BusinessRule { get; }

    public BusinessRuleValidationException(IBusinessRule businessRule) : base(businessRule.Message)
    {
        BusinessRule = businessRule;
    }

    public override string ToString()
    {
        return $"{BusinessRule.GetType().FullName}: {BusinessRule.Message}";
    }
}