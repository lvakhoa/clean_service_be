namespace CleanService.Src.Utils;

public class Timestamp
{
    public static long GetTimeStamp(TimeType? type = null)
    {
        return GetTimeStamp(DateTime.Now, type);
    }

    public static long GetTimeStamp(DateTime date, TimeType? type = null)
    {
        var timestamp = type switch {
            TimeType.Millisecond => (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds,
            TimeType.Second => (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds,
            TimeType.Minute => (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalMinutes,
            TimeType.Hour => (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalHours,
            _ => (long)(date.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0)).TotalSeconds
        };
        return timestamp;
    }
}

public enum TimeType
{
    Millisecond,
    Second,
    Minute,
    Hour,
}