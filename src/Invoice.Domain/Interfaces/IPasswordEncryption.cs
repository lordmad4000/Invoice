namespace Invoice.Domain.Interfaces
{
    public interface IPasswordEncryption
    {
        string GenerateHash(string pass, string salt);
        string GenerateSalt(int length);
    }
}