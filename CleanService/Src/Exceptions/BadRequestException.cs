using CleanService.Src.Constant;

namespace CleanService.Src.Exceptions;

[Serializable]
public class BadRequestException : Exception
{
    public BadRequestException(string message, string? exceptionCode = ExceptionConvention.BadRequest) : base(message)
    {
        ExceptionCode = exceptionCode;
    }

    public string? ExceptionCode { get; }
}
