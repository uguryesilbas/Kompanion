namespace Kompanion.Application.MediatR.Commands;

public record DeleteBaseCommand<TResponse> : BaseCommand<TResponse>
{
    public bool IsPersistence { get; init; }
}