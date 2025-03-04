namespace CleanService.Src.Common;

public class ApiSuccessResult<T>
{
    public ApiSuccessResult() { }

    public ApiSuccessResult(int statusCode, string message, T? data = default)
    {
        StatusCode = statusCode;
        Message = message;
        Data = data;
    }

    public int StatusCode { get; set; }

    public string Message { get; set; }

    public T? Data { get; set; }

}
