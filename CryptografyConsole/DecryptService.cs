using Newtonsoft.Json;
using System.Security.Cryptography;

namespace CryptografyConsole;

public class DecryptService : CryptografyBase
{
    public static byte[] DecryptAndGetFile(string filePath)
    {
        var decryptObject = GetDecryptResult(filePath);

        var ivByteArr = Convert.FromBase64String(decryptObject.IV);

        var decryptedByteArr = Decrypt(decryptObject.Value, ivByteArr, _keyByteArr);

        return decryptedByteArr;
    }

    private static CryptResult GetDecryptResult(string filePath)
    {
        var fileBase64String = ReadFileToString(filePath);

        fileBase64String = Base64ToString(fileBase64String);

        var decryptObject = JsonConvert.DeserializeObject<CryptResult>(fileBase64String);
        
        if(decryptObject is null) throw new Exception("Invalid encrypted file!");

        return decryptObject;
    }

    private static byte[] Decrypt(string decriptedBase64, byte[] ivByteArr, byte[] keyByteArr)
    {
        Aes aes = Aes.Create();
        aes.Key = keyByteArr;
        aes.IV = ivByteArr;

        aes.Mode = CipherMode.CBC;
        ICryptoTransform decipher = aes.CreateDecryptor(aes.Key, aes.IV);

        using var ms = new MemoryStream(Convert.FromBase64String(decriptedBase64));
        using var cs = new CryptoStream(ms, decipher, CryptoStreamMode.Read);
        using var output = new MemoryStream();

        cs.CopyTo(output);

        return output.ToArray();
    }
}
