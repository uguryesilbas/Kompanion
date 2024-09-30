using MediatR;

namespace Kompanion.Application.MediatR.Commands;

public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : BaseCommand<TResponse>
{
}