namespace Kompanion.Domain.Interfaces;

public interface IBusinessRule
{
    bool IsBroken(CancellationToken cancellationToken = default);
    string Message { get; }
}