using CryptografyConsole;

var filesFolder = @"C:\dev\csharp\CryptografyConsole\CryptografyConsole\Files";
var filesEncryptedFolder = @"C:\dev\csharp\CryptografyConsole\CryptografyConsole\Files_Encrypted";
var filesDecryptedFolder = @"C:\dev\csharp\CryptografyConsole\CryptografyConsole\Files_Decrypted";

while (true)
{
    EncryptFolderTo(filesFolder, filesEncryptedFolder);

    DecryptFolderTo(filesEncryptedFolder, filesDecryptedFolder);

    Console.ReadLine();
}

static void EncryptFolderTo(string filesFolder, string cryptedFolder)
{
    var filesInfo = GetFilesFromFolder(filesFolder);

    foreach (var fileInfo in filesInfo)
    {
        var cryptedFileByteArray = EncryptService.EncryptAndGetFile(fileInfo.FullName);

        if (cryptedFileByteArray is null) continue;
        
        var newName = $"encrypted-{fileInfo.Name}";

        SaveFile(cryptedFileByteArray, newName, cryptedFolder);

        Console.WriteLine($"Crypted and Saved - {newName}");
    }
}

static void DecryptFolderTo(string encryptedFolder, string decryptedFolder)
{
    var filesInfo = GetFilesFromFolder(encryptedFolder);

    foreach (var fileInfo in filesInfo)
    {
        var decryptedFileByteArray = DecryptService.DecryptAndGetFile(fileInfo.FullName);

        if (decryptedFileByteArray is null) continue;

        var newName = fileInfo.Name.Replace("encrypted", "decrypted");

        SaveFile(decryptedFileByteArray, newName, decryptedFolder);

        Console.WriteLine($"Decrypted and Saved - {newName}");
    }
}

static FileInfo[] GetFilesFromFolder(string encryptedFolder)
{
    var dirInfo = new DirectoryInfo(encryptedFolder);

    var filesInfo = dirInfo.GetFiles();
    return filesInfo;
}

static void SaveFile(byte[] encodedFile, string fileName, string folderPath)
{
    var filePath = Path.Combine(folderPath, fileName);

    File.WriteAllBytes(filePath, encodedFile);
}