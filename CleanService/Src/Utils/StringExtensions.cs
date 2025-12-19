namespace CleanService.Src.Utils;

public static class StringExtensions
{
    public static Guid? ConvertToGuid(this string id)
    {
        try
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return new Guid(id);
        }
        catch
        {
            return null;
        }
    }
}
