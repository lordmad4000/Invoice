namespace Invoice.Application.Common.Interfaces.Persistance
{
    public interface ITokenService
    {
        string GenerateToken(string userId, string userEmail);
    }
}