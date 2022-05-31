namespace Users.Application.Interfaces
{
    public interface IPasswordService
    {
        string GeneratePassword(string userName, string password, int saltLength);

        bool IsCorrectPassword(string userName, string saltHashPassword, string password);
    }
}