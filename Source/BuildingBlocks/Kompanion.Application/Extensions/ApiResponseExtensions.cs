using Microsoft.AspNetCore.Http;
using Kompanion.Application.Models;
using Kompanion.Application.Wrappers;

namespace Kompanion.Application.Extensions;

public static class ApiResponseExtensions
{
    public static TResponse NotFound<TResponse>(this TResponse response) where TResponse : ApiResponse
    {
        response.HttpStatusCode = StatusCodes.Status404NotFound;
        return response;
    }

    public static TResponse Created<TResponse>(this TResponse response) where TResponse : ApiResponse
    {
        response.HttpStatusCode = StatusCodes.Status201Created;
        return response;
    }

    public static TResponse BadRequest<TResponse>(this TResponse response) where TResponse : ApiResponse
    {
        response.HttpStatusCode = StatusCodes.Status400BadRequest;
        return response;
    }

    public static TResponse Ok<TResponse>(this TResponse response) where TResponse : ApiResponse
    {
        response.HttpStatusCode = StatusCodes.Status200OK;
        return response;
    }


    public static TResponse AddError<TResponse>(this TResponse response, string description, string code = "") where TResponse : ApiResponse
    {
        response.Errors.Add(new ApiResponseErrorMessage(code, description));
        return response;
    }

    public static TResponse AddErrors<TResponse>(this TResponse response, ICollection<ApiResponseErrorMessage> errors) where TResponse : ApiResponse
    {
        if (errors is { Count: > 0 })
        {
            response.Errors.AddRange(errors);
        }

        return response;
    }

    public static TResponse AddWarning<TResponse>(this TResponse response, string description, string code = "") where TResponse : ApiResponse
    {
        response.Warnings.Add(new ApiResponseWarningMessage(code, description));
        return response;
    }

    public static TResponse AddWarnings<TResponse>(this TResponse response, ICollection<ApiResponseWarningMessage> warnings) where TResponse : ApiResponse
    {
        if (warnings is { Count: > 0 })
        {
            response.Warnings.AddRange(warnings);
        }

        return response;
    }

    public static TResponse AddInformation<TResponse>(this TResponse response, string description, string code = "") where TResponse : ApiResponse
    {
        response.Informations.Add(new ApiResponseInformationMessage(code, description));
        return response;
    }

    public static TResponse AddInformations<TResponse>(this TResponse response, ICollection<ApiResponseInformationMessage> informations) where TResponse : ApiResponse
    {
        if (informations is { Count: > 0 })
        {
            response.Informations.AddRange(informations);
        }

        return response;
    }
}