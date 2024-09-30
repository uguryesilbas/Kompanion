namespace Kompanion.Application.Models;

public sealed record ApiResponseInformationMessage(string Message, string Code) : ApiResponseMessage(Message, Code);