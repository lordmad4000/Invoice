namespace Invoice.Infra.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string userEmail);
    }
}