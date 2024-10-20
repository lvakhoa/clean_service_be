using System.Net;

namespace CleanService.Src.Helpers;

public class ExceptionResponse : Exception
{
    public HttpStatusCode StatusCode { get; set; }
    
    public string ExceptionCode { get; set; }
    
    public ExceptionResponse(HttpStatusCode statusCode, string message, string exceptionCode) : base(message)
    {
        StatusCode = statusCode;
        ExceptionCode = exceptionCode;
    }
}