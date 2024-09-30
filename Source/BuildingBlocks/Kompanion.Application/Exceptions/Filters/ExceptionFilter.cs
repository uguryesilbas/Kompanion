using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Runtime.CompilerServices;
using Kompanion.Application.Extensions;
using Kompanion.Application.Models;
using Kompanion.Application.Wrappers;
using Kompanion.Domain.Exceptions;

[assembly: InternalsVisibleTo("Kompanion.BuildingBlocks.UnitTests")]

namespace Kompanion.Application.Exceptions.Filters;

internal sealed class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.ExceptionHandled = true;

        switch (context.Exception)
        {
            case HandledException handledException: 
                {
                    ApiResponse response = new()
                    {
                        HttpStatusCode = handledException.HttpStatusCode,
                        Errors = handledException.Errors
                    };

                    context.Result = new ObjectResult(response)
                    {
                        StatusCode = handledException.HttpStatusCode
                    };

                    break;
                }

            case NotFoundException notFoundException:
                {
                    ApiResponse response = new()
                    {
                        HttpStatusCode = notFoundException.HttpStatusCode,
                        Errors = notFoundException.Errors
                    };

                    context.Result = new ObjectResult(response)
                    {
                        StatusCode = notFoundException.HttpStatusCode
                    };

                    break;
                }

            case ValidationException validationException:
                {
                    ApiResponse response = new()
                    {
                        HttpStatusCode = StatusCodes.Status400BadRequest,
                        Errors = validationException.Errors.Select(x => new ApiResponseErrorMessage(x.ErrorMessage, x.ErrorCode)).ToList()
                    };

                    context.Result = new ObjectResult(response)
                    {
                        StatusCode = response.HttpStatusCode
                    };

                    break;
                }

            case BusinessRuleValidationException businessRuleException:
                {
                    ApiResponse response = ApiResponse.Create().BadRequest().AddError(businessRuleException.Message, "business_rule_exception");

                    context.Result = new ObjectResult(response)
                    {
                        StatusCode = response.HttpStatusCode
                    };

                    break;
                }

            default:
                {
                    context.Result = new ObjectResult("An error has occurred!")
                    {
                        StatusCode = StatusCodes.Status500InternalServerError
                    };

                    break;
                }
        }
    }
}