using Newtonsoft.Json;
using System.Security.Cryptography;

namespace CryptografyConsole;

public class EncryptService : CryptografyBase
{
    public static byte[] EncryptAndGetFile(string filePath)
    {
        var fileByteArray = ReadFileToByteArray(filePath);

        (var value, var iv) = Encrypt(fileByteArray, _keyByteArr);

        var cryptResult = new CryptResult(iv, value);

        var cryptJson = JsonConvert.SerializeObject(cryptResult);

        var base64OfJson = StringToBase64(cryptJson);
        var finalBase64 = StringToBase64(base64OfJson);

        return Convert.FromBase64String(finalBase64);
    }

    private static (string value, string iv) Encrypt(byte[] fileByteArr, byte[] keyByteArr)
    {
        byte[] encryptedByteArr;

        var aes = Aes.Create();
        aes.Key = keyByteArr;
        aes.GenerateIV();
        aes.Mode = CipherMode.CBC;
        ICryptoTransform cipher = aes.CreateEncryptor(aes.Key, aes.IV);

        using (var ms = new MemoryStream())
        {
            using (var cs = new CryptoStream(ms, cipher, CryptoStreamMode.Write))
            {
                cs.Write(fileByteArr);
            }

            encryptedByteArr = ms.ToArray();
        }

        var iv = Convert.ToBase64String(aes.IV);
        var value = Convert.ToBase64String(encryptedByteArr);

        return (value, iv);
    }
}
