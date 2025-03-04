using CleanService.Src.Constant;

namespace CleanService.Src.Exceptions;

public class UnprocessableRequestException : Exception
{
    public UnprocessableRequestException(string message, string[]? errors = null,
        string? exceptionCode = ExceptionConvention.UnprocessableRequest) : base(message)
    {
        ExceptionCode = exceptionCode;
        Errors = errors;
    }

    public string? ExceptionCode { get; }

    public string[]? Errors { get; set; }
}
