using System.Net;

namespace CleanService.Src.Utils;

public class SuccessResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public string Message { get; set; }
    public object? Data { get; set; }
}