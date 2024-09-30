using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Kompanion.Application.Wrappers;

namespace Kompanion.Application.MediatR.Behaviors;

internal sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IBaseRequest where TResponse : ApiResponse
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            ValidationContext<TRequest> validationContext = new ValidationContext<TRequest>(request);

            ValidationResult[] validationResults = await Task.WhenAll(_validators.Select(x => x.ValidateAsync(validationContext, cancellationToken)));

            List<ValidationFailure> validationFailures = validationResults.SelectMany(x => x.Errors).Where(p => p is not null).ToList();

            if (validationFailures is { Count: > 0 })
            {
                throw new ValidationException(validationFailures);
            }
        }

        return await next();
    }
}