using MediatR;

namespace Kompanion.Application.MediatR.Queries;

public abstract record BaseQuery<TResponse> : IRequest<TResponse>;