namespace Kompanion.Application.Models;

public sealed record ApiResponseErrorMessage(string Message, string Code) : ApiResponseMessage(Message, Code);