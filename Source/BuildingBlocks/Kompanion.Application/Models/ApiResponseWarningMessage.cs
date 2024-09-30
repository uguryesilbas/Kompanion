namespace Kompanion.Application.Models;

public sealed record ApiResponseWarningMessage(string Message, string Code) : ApiResponseMessage(Message, Code);