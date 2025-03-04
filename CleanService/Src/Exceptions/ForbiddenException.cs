using CleanService.Src.Constant;

namespace CleanService.Src.Exceptions;

[Serializable]
public class ForbiddenException : Exception
{
    public ForbiddenException(string message, string? exceptionCode = ExceptionConvention.Forbidden) : base(message)
    {
        ExceptionCode = exceptionCode;
    }

    public string? ExceptionCode { get; }
}
