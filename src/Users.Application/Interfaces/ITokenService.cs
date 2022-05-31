namespace Users.Application.Interfaces
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string userEmail, string secretKey);
    }
}