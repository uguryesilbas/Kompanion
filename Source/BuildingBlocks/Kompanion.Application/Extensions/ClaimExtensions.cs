using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Kompanion.Application.Extensions;

public static class ClaimExtensions
{
    public static int GetUserId(this IHttpContextAccessor contextAccessor, string claimType = ClaimTypes.NameIdentifier)
    {
        string value = contextAccessor.FindFirst(claimType);

        return string.IsNullOrWhiteSpace(value) ? 0 : value.As<int>();
    }

    public static string GetEmail(this IHttpContextAccessor contextAccessor, string claimType = ClaimTypes.Email)
    {
        return contextAccessor.FindFirst(claimType);
    }

    public static string GetUsername(this IHttpContextAccessor contextAccessor, string claimType = ClaimTypes.Name)
    {
        return contextAccessor.FindFirst(claimType);
    }

    public static string FindFirst(this IHttpContextAccessor contextAccessor, string claimType)
    {
        return contextAccessor.HttpContext?.User?.FindFirst(claimType)?.Value;
    }
}