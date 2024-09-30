using MediatR;

namespace Kompanion.Application.MediatR.Commands;

public abstract record BaseCommand<TResponse> : IRequest<TResponse>;