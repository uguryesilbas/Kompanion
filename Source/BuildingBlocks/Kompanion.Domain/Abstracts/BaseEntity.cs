using Kompanion.Domain.Exceptions;
using Kompanion.Domain.Interfaces;

namespace Kompanion.Domain.Abstracts;

public abstract class BaseEntity
{
    public abstract int Id { get; set; }

    public static void CheckRule(IBusinessRule rule, CancellationToken cancellationToken = default)
    {
        if (rule.IsBroken(cancellationToken))
        {
            throw new BusinessRuleValidationException(rule);
        }
    }
}