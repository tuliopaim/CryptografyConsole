namespace CryptografyConsole;

public class CryptResult
{
    public CryptResult(string iv, string value)
    {
        IV = iv;
        Value = value;
    }

    public string IV { get; init; }
    public string Value { get; init; }
}