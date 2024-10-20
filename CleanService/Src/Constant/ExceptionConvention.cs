namespace CleanService.Src.Constant;

public static class ExceptionConvention
{
    public const string UniqueConstraintViolation = "ERR/UNIQUE_CONSTRAINT_VIOLATION";
    public const string MaxLengthViolation = "ERR/MAX_LENGTH_VIOLATION";
    public const string NotFound = "ERR/NOT_FOUND";
    public const string Unauthorized = "ERR/UNAUTHORIZED";
    public const string Forbidden = "ERR/FORBIDDEN";
    public const string InternalServerError = "ERR/INTERNAL_SERVER_ERROR";
}