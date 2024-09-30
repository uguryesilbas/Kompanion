using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Kompanion.Application.Extensions;
using Kompanion.Application.Models;

namespace Kompanion.Application.Wrappers;

public record ApiResponse<TData>(TData Data) : ApiResponse;

public record ApiResponse
{
    public int HttpStatusCode { get; set; } = StatusCodes.Status200OK;
    public List<ApiResponseErrorMessage> Errors { get; set; } = new();
    public List<ApiResponseInformationMessage> Informations { get; set; } = new();
    public List<ApiResponseWarningMessage> Warnings { get; set; } = new();


    public static TResponse Create<TResponse>(ICollection<ApiResponseErrorMessage> errors = null,
        ICollection<ApiResponseInformationMessage> informations = null,
        ICollection<ApiResponseWarningMessage> warnings = null) where TResponse : ApiResponse, new()
    {
        TResponse response = new();

        if (errors is { Count: > 0 })
        {
            response.AddErrors(errors);
        }

        if (informations is { Count: > 0 })
        {
            response.AddInformations(informations);
        }

        if (warnings is { Count: > 0 })
        {
            response.AddWarnings(warnings);
        }

        return response;
    }

    public static ApiResponse Create(ICollection<ApiResponseErrorMessage> errors = null,
        ICollection<ApiResponseInformationMessage> informations = null,
        ICollection<ApiResponseWarningMessage> warnings = null)
    {
        return Create<ApiResponse>(errors, informations, warnings);
    }

    public static TResponse Create<TResponse>(ModelStateDictionary modelState) where TResponse : ApiResponse, new()
    {
        TResponse response = new();
        response.AddErrors(modelState
            .SelectMany(x => x.Value?.Errors.Select(s => new ApiResponseErrorMessage(s.ErrorMessage, "0")) ?? Array.Empty<ApiResponseErrorMessage>())
            .ToList());

        return response;
    }

    public static ApiResponse Create(ModelStateDictionary modelState)
    {
        return Create<ApiResponse>(modelState);
    }
}

