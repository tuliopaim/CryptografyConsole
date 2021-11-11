using System.Text;

namespace CryptografyConsole;

public class CryptografyBase
{
    private static readonly string _encryptKey = "C&F)J@NcRfUjXn2r5u8x/A?D*G-KaPdS";
    private static readonly string _encryptKey64 = StringToBase64(_encryptKey);

    protected static readonly byte[] _keyByteArr = Convert.FromBase64String(_encryptKey64);

    protected static string ReadFileToString(string filePath)
    {
        var fileByteArr = File.ReadAllBytes(filePath);

        return Encoding.ASCII.GetString(fileByteArr);
    }

    protected static string ReadFileToBase64(string filePath)
    {
        var fileByteArr = File.ReadAllBytes(filePath);

        return Convert.ToBase64String(fileByteArr);
    }

    protected static byte[] ReadFileToByteArray(string filePath)
    {
        return File.ReadAllBytes(filePath);
    }

    protected static string Base64ToString(string value)
    {
        return Encoding.ASCII.GetString(Convert.FromBase64String(value));
    }

    protected static string StringToBase64(string value)
    {
        return Convert.ToBase64String(Encoding.ASCII.GetBytes(value));
    }
}
