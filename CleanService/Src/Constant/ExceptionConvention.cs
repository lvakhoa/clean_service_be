namespace CleanService.Src.Constant;

public static class ExceptionConvention
{
    public const string UniqueConstraintViolation = "ERR/UNIQUE_CONSTRAINT_VIOLATION";
    public const string MaxLengthViolation = "ERR/MAX_LENGTH_VIOLATION";
    public const string ValidationFailed = "ERR/VALIDATION_FAILED";
    public const string NotFound = "ERR/NOT_FOUND";
    public const string Unauthorized = "ERR/UNAUTHORIZED";
    public const string Forbidden = "ERR/FORBIDDEN";
    public const string InternalServerError = "ERR/INTERNAL_SERVER_ERROR";
    public const string BookingStatusNotCompleted  = "ERR/BOOKING_STATUS_NOT_COMPLETED";
    public const string ItemAlreadyExist = "ERR/ITEM_ALREADY_EXISTS";
}