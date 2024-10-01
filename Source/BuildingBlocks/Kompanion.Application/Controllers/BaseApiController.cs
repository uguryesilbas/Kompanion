using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Kompanion.Application.Wrappers;

namespace Kompanion.Application.Controllers;

public abstract class BaseApiController : ControllerBase
{
    protected const string DefaultApiRoute = "api/v{version:apiVersion}";

    private readonly ISender _sender;
    protected BaseApiController(ISender sender)
    {
        _sender = sender;
    }
    protected async Task<IActionResult> Send<TRequest>(TRequest request, int statusCode = StatusCodes.Status200OK, CancellationToken cancellationToken = default) where TRequest : class
    {
        if (!ModelState.IsValid)
        {
            return new ObjectResult(ApiResponse.Create(ModelState))
            {
                StatusCode = StatusCodes.Status400BadRequest
            };
        }

        object result = await _sender.Send(request, cancellationToken);

        if (result is ApiResponse apiResponse)
        {
            return new ObjectResult(apiResponse)
            {
                StatusCode = apiResponse.HttpStatusCode
            };
        }

        return StatusCode(statusCode, result);
    }
}