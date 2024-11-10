using System.Security.Cryptography;

namespace CleanService.Src.Utils.Crypto;

public class HmacHelper
{
    public static string Compute(HmacAlgo algorithm = HmacAlgo.HMACSHA256, string key = "",
        string message = "")
    {
        byte[] keyByte = System.Text.Encoding.UTF8.GetBytes(key);
        byte[] messageBytes = System.Text.Encoding.UTF8.GetBytes(message);
        byte[]? hashMessage = null;

        switch (algorithm)
        {
            case HmacAlgo.HMACMD5:
                hashMessage = new HMACMD5(keyByte).ComputeHash(messageBytes);
                break;
            case HmacAlgo.HMACSHA1:
                hashMessage = new HMACSHA1(keyByte).ComputeHash(messageBytes);
                break;
            case HmacAlgo.HMACSHA256:
                hashMessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
                break;
            case HmacAlgo.HMACSHA512:
                hashMessage = new HMACSHA512(keyByte).ComputeHash(messageBytes);
                break;
            default:
                hashMessage = new HMACSHA256(keyByte).ComputeHash(messageBytes);
                break;
        }

        return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
    }
}

public enum HmacAlgo
{
    HMACMD5,
    HMACSHA1,
    HMACSHA256,
    HMACSHA512
}