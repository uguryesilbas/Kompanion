using MediatR;

namespace Kompanion.Application.MediatR.Queries;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : BaseQuery<TResponse>
{
}